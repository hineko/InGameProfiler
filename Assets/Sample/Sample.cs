using InGameProfiling;
using UnityEngine;

public class Sample : MonoBehaviour
{
	private InGameProfiler _profiler;
	private bool _isProfiling;

	private void Awake()
	{
		// グラフの描画する場所を指定する
		_profiler = new InGameProfiler(new Rect(30, 30, Screen.width - 60, Screen.height - 60));
		_isProfiling = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F3))
		{
			_isProfiling = !_isProfiling;
		}
	}

	private void LateUpdate()
	{
		if (_isProfiling)
		{
			// 計測更新
			_profiler?.ProfilerLateUpdate();
		}
	}

	private void OnGUI()
	{
		if (_isProfiling)
		{
			// グラフの描画更新
			_profiler?.OnGUI();
		}
	}
}
