using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Profiling;

namespace InGameProfiling
{
	public class InGameProfiler
	{
		private const string ToStringF1 = "F1";

		// サンプル数 Profilerと同じ300フレーム
		public const int SampleProfileNumber = 300;

		private const float SizeMegaByte = 1024f * 1024f;

		private const float TitleHeight = 20;
		private const float LableWidth = 30;
		private const float LableHeight = 10f;

		private GUIStyle _backgroundStyle;
		private GUIStyle _textStyle;
		private GUIStyle _graphLabel;

		// GL描画用
		private Material _lineMaterial;

		private GraphRecorder _scriptUpdateRecorder;
		private GraphRecorder _otherGraphRecorder;
		private GraphData _heapMemoryGraph;
		private GraphData _usedMemoryGraph;

		private GraphData _loopScriptData;
		private GraphData _loopOtherData;
		private GraphData _loopAnimationData;
		private GraphData _loopPhysicsData;
		private GraphData _loopRenderData;

		private readonly Rect _drawRect;

		public InGameProfiler(Rect rect)
		{
			_drawRect = rect;
			StyleInitialize();
			RecorderInitialize();
			PlayerLoopInitialize();
		}

		public void ProfilerUpdate() { }

		/// <summary>
		/// 計測
		/// </summary>
		public void ProfilerLateUpdate()
		{
			RecorderUpdate();
			PlayerLoopUpdate();
			MemoryRecordUpdate();
		}

		/// <summary>
		/// GUIStyleの準備とGLの描画準備
		/// </summary>
		private void StyleInitialize()
		{
			// GL描画用マテリアル設定
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			_lineMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
			_lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			_lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			_lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			_lineMaterial.SetInt("_ZWrite", 0);

			_backgroundStyle = new GUIStyle() { normal = { background = new Texture2D(1, 1, TextureFormat.ARGB32, false) } };
			_backgroundStyle.normal.background.SetPixel(1, 1, new Color(0f, 0f, 0f, 0.5f));
			_backgroundStyle.normal.background.Apply();

			_textStyle = new GUIStyle() { fontSize = 11, alignment = TextAnchor.UpperLeft, normal = { textColor = Color.white } };
			_graphLabel = new GUIStyle() { fontSize = 10, alignment = TextAnchor.UpperRight, normal = { textColor = Color.white } };
		}

		private void RecorderInitialize()
		{
			// Script
			_scriptUpdateRecorder = new GraphRecorder("ScriptUpdate", Color.cyan, SampleProfileNumber,
				ProfilingConst.Update.ScriptRunBehaviourUpdate,
				ProfilingConst.PreLateUpdate.ScriptRunBehaviourLateUpdate,
				ProfilingConst.FixedUpdate.ScriptRunBehaviourFixedUpdate,
				ProfilingConst.Update.ScriptRunDelayedDynamicFrameRate);

			// CPU
			string[] array = new string[ProfilingConst.GetAllTargetLength()];
			string[][] names = ProfilingConst.AllTargetNames;
			int index = 0;
			for (int y = 0; y < names.Length; y++)
			{
				for (int x = 0; x < names[y].Length; x++)
				{
					array[index++] = names[y][x];
				}
			}
			_otherGraphRecorder = new GraphRecorder("Other", Color.magenta, SampleProfileNumber, array);

			// メモリーグラフ
			_heapMemoryGraph = new GraphData("HeapMemory", Color.green, SampleProfileNumber);
			_usedMemoryGraph = new GraphData("UsedMemory", Color.yellow, SampleProfileNumber);
		}

		/// <summary>
		/// PlayerLoopによる計測結果を受け取る変数の作成
		/// </summary>
		private void PlayerLoopInitialize()
		{
			_loopScriptData = new GraphData("PlayerLoopScript", Color.red, SampleProfileNumber);
			_loopOtherData = new GraphData("PlayerLoop", Color.white, SampleProfileNumber);
			_loopAnimationData = new GraphData("PlayerAnimation", Color.yellow, SampleProfileNumber);
			_loopPhysicsData = new GraphData("PlayerPhysics", Color.green, SampleProfileNumber);
			_loopRenderData = new GraphData("PlayerRender", Color.blue, SampleProfileNumber);
		}

		/// <summary>
		/// PlayerLoopの計測結果をミリ秒に変換して保存する
		/// </summary>
		private void PlayerLoopUpdate()
		{
			// 計測結果を受け取る 秒で帰ってくる
			float allTime = PlayerLoopProfiler.GetLastExecuteTime();
			float scriptUpdateTime = PlayerLoopProfiler.GetProfilingTime<Update.ScriptRunBehaviourUpdate>() +
									 PlayerLoopProfiler.GetProfilingTime<PreLateUpdate.ScriptRunBehaviourLateUpdate>() +
									 PlayerLoopProfiler.GetProfilingTime<FixedUpdate.ScriptRunBehaviourFixedUpdate>() +
									 PlayerLoopProfiler.GetProfilingTime<Update.ScriptRunDelayedDynamicFrameRate>();

			float animationTime = PlayerLoopProfiler.GetProfilingTime<PreLateUpdate.DirectorUpdateAnimationBegin>() +
								  PlayerLoopProfiler.GetProfilingTime<PreLateUpdate.DirectorUpdateAnimationEnd>();
			float physicsTime = PlayerLoopProfiler.GetProfilingTime<FixedUpdate.PhysicsFixedUpdate>();
			float renderTime = PlayerLoopProfiler.GetProfilingTime<PostLateUpdate.FinishFrameRendering>();

			// ミリ秒に変換
			_loopScriptData.Update(scriptUpdateTime * 1000f);
			_loopAnimationData.Update(animationTime * 1000f);
			_loopPhysicsData.Update(physicsTime * 1000f);
			_loopRenderData.Update(renderTime * 1000f);
			_loopOtherData.Update(allTime * 1000f);
		}

		/// <summary>
		/// Recorderによる計測結果の受け取りと保存
		/// </summary>
		private void RecorderUpdate()
		{
			// 内部で計測結果を記録する
			_scriptUpdateRecorder.Update();
			_otherGraphRecorder.Update();
		}

		/// <summary>
		/// メモリ使用量の記録
		/// </summary>
		private void MemoryRecordUpdate()
		{
			_heapMemoryGraph.Update(Profiler.GetMonoHeapSizeLong() / SizeMegaByte);
			_usedMemoryGraph.Update(Profiler.GetMonoUsedSizeLong() / SizeMegaByte);
		}

		/// <summary>
		/// グラフを描画する
		/// </summary>
		public void OnGUI()
		{
			GUI.Box(_drawRect, "", _backgroundStyle);
			var rect = new Rect(_drawRect.position.x + 30, _drawRect.position.y + 30, 500, 100);

			DrawMultiSampleGraph(rect, "Script", "msec", 5, 0.05f, _scriptUpdateRecorder.GetGraph(), _loopScriptData);
			rect.y += 120;

			DrawMultiSampleGraph(rect, "CPU", "msec", 5, 1f, _loopOtherData, _otherGraphRecorder.GetGraph());
			rect.y += 120;

			DrawMultiSampleGraph(rect, "PlayerLoop", "msec", 5, 1f, _loopAnimationData, _loopRenderData, _loopPhysicsData);
			rect.y += 120;

			DrawMultiSampleGraph(rect, "Memory", "MB", 5, 1, _heapMemoryGraph, _usedMemoryGraph);
			rect.y += 120;
		}

		/// <summary>
		/// グラフを描画する
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="title"></param>
		/// <param name="unitName"></param>
		/// <param name="sections"></param>
		/// <param name="minInterVal"></param>
		/// <param name="datas"></param>
		private void DrawMultiSampleGraph(Rect rect, string title, string unitName, int sections, float minInterVal, params GraphData[] datas)
		{
			GUI.color = Color.white;

			// 背景
			GUI.Box(rect, "", _backgroundStyle);
			Rect labelRect = new Rect(rect.x + 3.0f, rect.y - 15.0f, rect.width, TitleHeight);

			// グラフ名
			GUI.Label(labelRect, title, _textStyle);

			// 単位名
			labelRect.x += rect.width;
			labelRect.y += rect.height;
			GUI.Label(labelRect, unitName, _textStyle);

			if (Event.current.type == EventType.Repaint)
			{
				float collectPosX = rect.x;
				float collectPosY = rect.y;

				float maxValue = 0;
				labelRect = new Rect(rect.x + rect.width + 10, rect.y + 2f, 100, TitleHeight);

				// グラフ上限を決定する
				for (int i = 0; i < datas.Length; i++)
				{
					GUI.color = datas[i].Color;
					GUI.Label(labelRect, datas[i].Name);
					labelRect.y += TitleHeight;
					//for (int j = 0; j < datas[i].Length; j++)
					{
						var tempMax = Mathf.Max(datas[i].Data) * 1.5f;
						if (tempMax > maxValue)
						{
							maxValue = tempMax;
						}
					}
				}

				GUI.color = Color.white;

				float interval = (int)(maxValue / sections);
				if (interval < minInterVal)
				{
					interval = minInterVal;
				}

				float cellHeight;
				int gridCount;

				// グラフ描画
				GL.PushMatrix();
				{
					_lineMaterial.SetPass(0);
					//GL.MultMatrix(transform.localToWorldMatrix);

					// 補助線
					GL.Begin(GL.LINES);
					{
						cellHeight = rect.height * interval / maxValue;
						gridCount = (int)(maxValue / interval);
						var ans = maxValue % interval;
						if (!(ans < 0 || 0 < ans))
						{
							gridCount -= 1;
						}

						if (gridCount >= 10)
						{
							cellHeight = rect.height / 10f;
							gridCount = 10;
						}

						for (int i = 1; i <= gridCount; i++)
						{
							var line = collectPosY + rect.height - i * cellHeight;
							GL.Vertex3(collectPosX, line, 0);
							GL.Vertex3(collectPosX + rect.width, line, 0);
						}
					}
					GL.End();

					for (int i = 0; i < datas.Length; i++)
					{
						// データ
						GL.Begin(GL.LINE_STRIP);
						{
							Color color = datas[i].Color;

							for (int j = 0; j < datas[i].Data.Length; j++)
							{
								GL.Color(color);
								GL.Vertex3(
									collectPosX + rect.width / (datas[i].Data.Length - 1) * j,
									collectPosY + rect.height * (1 - datas[i].Data[j] / maxValue),
									0);
							}
						}
						GL.End();
					}
				}
				GL.PopMatrix();

				// 補助線の数値
				rect.xMin -= LableWidth;
				labelRect = new Rect(rect.x - 4.0f, rect.y - 6.0f, LableWidth, LableHeight);
				GUI.Label(labelRect, maxValue.ToString(ToStringF1), _graphLabel);
				for (int i = 1; i <= gridCount; i++)
				{
					labelRect.y = rect.y + rect.height - i * cellHeight - 6.0f;
					if (labelRect.y > rect.y)
					{
						GUI.Label(labelRect, (interval * i).ToString(ToStringF1), _graphLabel);
					}
				}
			}
		}
	}
}