using System;
using TMPro;
using UniSimpleProfiler.Internal;
using UnityEngine;
using UnityEngine.Profiling;

namespace UniSimpleProfiler
{
	/// <summary>
	/// シンプルなプロファイラの UI を管理するクラス
	/// </summary>
	[DisallowMultipleComponent]
	public sealed partial class UniSimpleProfilerUI : MonoBehaviour
	{
		//====================================================================================
		// 定数
		//====================================================================================
		private const float INTERVAL = 0.5f;

		//====================================================================================
		// 変数(SerializeField)
		//====================================================================================
		[SerializeField] private TextMeshProUGUI	m_fpsTextUI				= null;
		[SerializeField] private TextMeshProUGUI	m_gcTextUI				= null;
		[SerializeField] private TextMeshProUGUI	m_monoUsedTextUI		= null;
		[SerializeField] private TextMeshProUGUI	m_monoTotalTextUI		= null;
		[SerializeField] private TextMeshProUGUI	m_unityUsedTextUI		= null;
		[SerializeField] private TextMeshProUGUI	m_unityTotalTextUI		= null;

		//====================================================================================
		// 変数(readonly)
		//====================================================================================
		private readonly FPSCounter	m_fPSCounter = new FPSCounter();

		//====================================================================================
		// 変数
		//====================================================================================
		private float m_timer;

		//====================================================================================
		// 関数
		//====================================================================================
		/// <summary>
		/// 更新される時に呼び出されます
		/// </summary>
		private void Update()
		{
			m_fPSCounter.Update();

			m_timer += Time.unscaledDeltaTime;
			if ( m_timer < INTERVAL ) return;
			m_timer -= INTERVAL;

			var monoUsed	= ( Profiler.GetMonoUsedSizeLong()			>> 10 ) / 1024f;
			var monoTotal	= ( Profiler.GetMonoHeapSizeLong()			>> 10 ) / 1024f;
			var unityUsed	= ( Profiler.GetTotalAllocatedMemoryLong()	>> 10 ) / 1024f;
			var unityTotal	= ( Profiler.GetTotalReservedMemoryLong()	>> 10 ) / 1024f;

			m_fpsTextUI			.SetText( "{0}", ( int ) m_fPSCounter.Fps	);
			m_gcTextUI			.SetText( "{0}", GC.CollectionCount( 0 )	);
			m_monoUsedTextUI	.SetText( "{0}", ( int ) monoUsed			);
			m_monoTotalTextUI	.SetText( "{0}", ( int ) monoTotal			);
			m_unityUsedTextUI	.SetText( "{0}", ( int ) unityUsed			);
			m_unityTotalTextUI	.SetText( "{0}", ( int ) unityTotal			);
		}
	}
}