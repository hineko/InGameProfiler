using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.LowLevel;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Profiling;

namespace InGameProfiling
{
	public static class PlayerLoopProfiler
	{
		public class Profiling
		{
			public float StartTime { get; private set; }
			public float ExecuteTime { get; private set; }
			public float EndTime { get; private set; }

			public void Start()
			{
				// ゲームが起動してからの時間（秒）を記録する
				StartTime = Time.realtimeSinceStartup;
			}

			public void End()
			{
				EndTime = Time.realtimeSinceStartup;
				ExecuteTime = EndTime - StartTime;
			}

			public void Reset()
			{
				ExecuteTime = 0;
			}
		}

		private static bool _isRecording;
		private static readonly Dictionary<string, CustomSampler> Samplers = new Dictionary<string, CustomSampler>();

		private static float _loopStartTime;
		private static float _prevLoopExecuteTime;

#if UNITY_2018_1_OR_NEWER

		private static readonly Dictionary<Type, Profiling> ProfilingDictionary = new Dictionary<Type, Profiling>();
		private static readonly Dictionary<Type, float> PrevSubSystemExecuteTimeDictionary = new Dictionary<Type, float>();

		// 各Awakeよりも先に、ゲーム起動時に呼ばれる属性
		[RuntimeInitializeOnLoadMethod]
		private static void Setup()
		{
			// 計測する項目
			Type[] profilePoints =
			{
				typeof(Initialization.PlayerUpdateTime),

				typeof(Update.ScriptRunBehaviourUpdate),
				typeof(PreLateUpdate.ScriptRunBehaviourLateUpdate),
				typeof(FixedUpdate.ScriptRunBehaviourFixedUpdate),
				typeof(Update.ScriptRunDelayedDynamicFrameRate),

				typeof(PreLateUpdate.DirectorUpdateAnimationBegin),
				typeof(PreLateUpdate.DirectorUpdateAnimationEnd),

				typeof(PostLateUpdate.UpdateAllRenderers),
				typeof(PostLateUpdate.UpdateAllSkinnedMeshes),

				typeof(PostLateUpdate.FinishFrameRendering),

				typeof(FixedUpdate.PhysicsFixedUpdate)
			};

			PlayerLoopSystem playerLoop = PlayerLoop.GetDefaultPlayerLoop();
			AppendProfilingLoopSystem(ref playerLoop, profilePoints);

			// 計測機能を追加したPlayerLoopをセットする
			PlayerLoop.SetPlayerLoop(playerLoop);
		}

		private static void AppendProfilingLoopSystem(ref PlayerLoopSystem loopSystem, Type[] profilePoints)
		{
			for (int i = 0; i < profilePoints.Length; i++)
			{
				ProfilingDictionary.Add(profilePoints[i], new Profiling());
				PrevSubSystemExecuteTimeDictionary.Add(profilePoints[i], 0.0f);
			}

			// 処理末端なければ登録
			Type finishType = typeof(PostLateUpdate.FinishFrameRendering);
			if (!ProfilingDictionary.ContainsKey(finishType))
			{
				ProfilingDictionary.Add(finishType, new Profiling());
				PrevSubSystemExecuteTimeDictionary.Add(finishType, 0.0f);
			}

			List<PlayerLoopSystem> newSystems = new List<PlayerLoopSystem>();
			for (int i = 0; i < loopSystem.subSystemList.Length; i++)
			{
				PlayerLoopSystem subSystem = loopSystem.subSystemList[i];
				newSystems.Clear();

				if (i == 0)
				{
					// 処理先頭で時間を計測する
					newSystems.Add(new PlayerLoopSystem { updateDelegate = LoopStartPoint });
				}

				for (int j = 0; j < subSystem.subSystemList.Length; j++)
				{
					PlayerLoopSystem secondSub = subSystem.subSystemList[j];

					Profiling profiling = null;
					if (ProfilingDictionary.TryGetValue(secondSub.type, out profiling))
					{
						// 対象の処理の前後に計測用のループシステムを差し込む
						newSystems.Add(new PlayerLoopSystem
						{
							type = typeof(Profiling),
							updateDelegate = profiling.Start
						});
						// もともとの処理
						newSystems.Add(secondSub);
						// 計測終了処理を差し込む
						newSystems.Add(new PlayerLoopSystem
						{
							type = typeof(Profiling),
							updateDelegate = profiling.End
						});
					}
					else
					{
						// 計測対象ではないループはそのまま戻す
						newSystems.Add(secondSub);
					}
				}

				// 末端到達
				if (i == loopSystem.subSystemList.Length - 1)
				{
					newSystems.Add(new PlayerLoopSystem() { updateDelegate = LoopEndPoint });
				}

				// 計測機能を差し込んだループシステムに上書きする
				subSystem.subSystemList = newSystems.ToArray();
				loopSystem.subSystemList[i] = subSystem;
			}
		}

		private static void LoopStartPoint()
		{
			_loopStartTime = Time.realtimeSinceStartup;
		}

		private static void LoopEndPoint()
		{
			Profiling finish = ProfilingDictionary[typeof(PostLateUpdate.FinishFrameRendering)];

			float endTime = finish.EndTime;
			foreach (var kv in ProfilingDictionary)
			{
				PrevSubSystemExecuteTimeDictionary[kv.Key] = kv.Value.ExecuteTime;
				kv.Value.Reset();
			}
			_prevLoopExecuteTime = endTime - _loopStartTime;
		}

		public static float GetLastExecuteTime()
		{
			return _prevLoopExecuteTime;
		}

		/// <summary>
		/// 処理にかかった時間を秒で返す
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static float GetProfilingTime<T>()
		{
			return GetProfilingTime(typeof(T));
		}

		public static float GetProfilingTime(Type t)
		{
			float time;
			if (PrevSubSystemExecuteTimeDictionary.TryGetValue(t, out time))
			{
				return time;
			}

			return 0.0f;
		}

#endif

		public static long GetElapsedNanoSeconds(string name)
		{
			if (Samplers.ContainsKey(name))
			{
				Recorder recorder = Samplers[name].GetRecorder();
				return recorder.elapsedNanoseconds;
			}

			return 0;
		}
	}
}