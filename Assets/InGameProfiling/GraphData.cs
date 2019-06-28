using UnityEngine;
using UnityEngine.Profiling;

namespace InGameProfiling
{
	public enum GraphKind
	{
		Script,
		CPU,
		Memory,
	}

	/// <summary>
	/// グラフ描画用のデータクラス
	/// </summary>
	public class GraphData
	{
		public readonly string Name;    // グラフ名
		public readonly float[] Data;	// 計測データ配列 Queueだと重かった
		public readonly Color Color;	// グラフの色

		public GraphData(string name, Color color, int sampleNum)
		{
			Name = name;
			Data = new float[sampleNum];
			Color = color;
		}

		/// <summary>
		/// データの更新
		/// </summary>
		/// <param name="data"></param>
		public void Update(float data)
		{
			// データを一つずつ前へずらす
			for (int i = 0; i < Data.Length; i++)
			{
				if (i + 1 < Data.Length)
				{
					Data[i] = Data[i + 1];
				}
			}

			// 最後尾に最新のデータを追加
			Data[Data.Length - 1] = data;
		}
	}

	public class GraphRecorder
	{
		private readonly GraphData _graph;
		private readonly CustomRecorder _recorder;

		public GraphRecorder(string name, Color color, int sampleNum, params string[] samplerNames)
		{
			_graph = new GraphData(name, color, sampleNum);
			_recorder = new CustomRecorder(samplerNames);
		}

		public void Update()
		{
			// データはナノ秒で撮れるので、ミリ秒に変換して記録
			var time = _recorder.GetElapsedNanoseconds() / 1000000F;
			_graph.Update(time);
		}

		public GraphData GetGraph()
		{
			return _graph;
		}
	}

	public class CustomRecorder
	{
		private readonly Recorder[] _recorders;

		public CustomRecorder(params string[] samplerNames)
		{
			_recorders = new Recorder[samplerNames.Length];

			for (int i = 0; i < _recorders.Length; i++)
			{
				_recorders[i] = Recorder.Get(samplerNames[i]);
			}
		}

		public float GetElapsedNanoseconds()
		{
			float result = 0.0f;
			for (int i = 0; i < _recorders.Length; i++)
			{
				result += _recorders[i].elapsedNanoseconds;
			}

			return result;
		}

		public int GetSampleBlockCount()
		{
			int result = 0;

			for (int i = 0; i < _recorders.Length; i++)
			{
				result += _recorders[i].sampleBlockCount;
			}

			return result;
		}
	}
}