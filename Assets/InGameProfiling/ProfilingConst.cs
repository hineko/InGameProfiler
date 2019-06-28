namespace InGameProfiling
{
	// PlayerLoopSystem定数クラス
	public class ProfilingConst
	{
		
		public class Initialization
		{
			public const string PlayerUpdateTime = "Initialization.PlayerUpdateTime"; // WaitForTargetFPS
			public const string AsyncUploadTimeSlicedUpdate = "Initialization.AsyncUploadTimeSlicedUpdate";
			public const string SynchronizeInputs = "Initialization.SynchronizeInputs";
			public const string SynchronizeState = "Initialization.SynchronizeState";
			public const string XREarlyUpdate = "Initialization.XREarlyUpdate";

			public static string[] TargetNames =
			{
				PlayerUpdateTime, AsyncUploadTimeSlicedUpdate, SynchronizeInputs, SynchronizeState, XREarlyUpdate,
			};
		}

		public class EarlyUpdate
		{
			public const string PollPlayerConnection = "EarlyUpdate.PollPlayerConnection";
			public const string ProfilerStartFrame = "EarlyUpdate.ProfilerStartFrame";
			public const string GpuTimestamp = "EarlyUpdate.GpuTimestamp";
			public const string AnalyticsCoreStatsUpdate = "EarlyUpdate.AnalyticsCoreStatsUpdate";
			public const string UnityWebRequestUpdate = "EarlyUpdate.UnityWebRequestUpdate";
			public const string ExecuteMainThreadJobs = "EarlyUpdate.ExecuteMainThreadJobs";
			public const string ProcessMouseInWindow = "EarlyUpdate.ProcessMouseInWindow";
			public const string ClearIntermediateRenderers = "EarlyUpdate.ClearIntermediateRenderers";
			public const string ClearLines = "EarlyUpdate.ClearLines";
			public const string PresentBeforeUpdate = "EarlyUpdate.PresentBeforeUpdate";
			public const string ResetFrameStatsAfterPresent = "EarlyUpdate.ResetFrameStatsAfterPresent";
			public const string UpdateAllUnityWebStreams = "EarlyUpdate.UpdateAllUnityWebStreams";
			public const string UpdateAsyncReadbackManager = "EarlyUpdate.UpdateAsyncReadbackManager";
			public const string UpdateStreamingManager = "EarlyUpdate.UpdateStreamingManager";
			public const string UpdateTextureStreamingManager = "EarlyUpdate.UpdateTextureStreamingManager";
			public const string UpdatePreloading = "EarlyUpdate.UpdatePreloading";
			public const string RendererNotifyInvisible = "EarlyUpdate.RendererNotifyInvisible";
			public const string PlayerCleanupCachedData = "EarlyUpdate.PlayerCleanupCachedData";
			public const string UpdateMainGameViewRect = "EarlyUpdate.UpdateMainGameViewRect";
			public const string UpdateCanvasRectTransform = "EarlyUpdate.UpdateCanvasRectTransform";
			public const string XRUpdate = "EarlyUpdate.XRUpdate";
			public const string UpdateInputManager = "EarlyUpdate.UpdateInputManager";
			public const string ProcessRemoteInput = "EarlyUpdate.ProcessRemoteInput";
			public const string TangoUpdate = "EarlyUpdate.TangoUpdate";
			public const string ScriptRunDelayedStartupFrame = "EarlyUpdate.ScriptRunDelayedStartupFrame";
			public const string UpdateKinect = "EarlyUpdate.UpdateKinect";
			public const string DeliverIosPlatformEvents = "EarlyUpdate.DeliverIosPlatformEvents";
			public const string DispatchEventQueueEvents = "EarlyUpdate.DispatchEventQueueEvents";
			public const string DirectorSampleTime = "EarlyUpdate.DirectorSampleTime";

			public const string PhysicsResetInterpolatedTransformPosition =
				"EarlyUpdate.PhysicsResetInterpolatedTransformPosition";

			public const string SpriteAtlasManagerUpdate = "EarlyUpdate.SpriteAtlasManagerUpdate";
			public const string PerformanceAnalyticsUpdate = "EarlyUpdate.PerformanceAnalyticsUpdate";

			public static string[] TargetNames =
			{
				PollPlayerConnection, ProfilerStartFrame, GpuTimestamp, AnalyticsCoreStatsUpdate,
				UnityWebRequestUpdate, ExecuteMainThreadJobs, ProcessMouseInWindow, ClearIntermediateRenderers,
				ClearLines, PresentBeforeUpdate, ResetFrameStatsAfterPresent, UpdateAllUnityWebStreams,
				UpdateAsyncReadbackManager, UpdateStreamingManager, UpdateTextureStreamingManager, UpdatePreloading,
				RendererNotifyInvisible, PlayerCleanupCachedData, UpdateMainGameViewRect, UpdateCanvasRectTransform,
				XRUpdate, UpdateInputManager, ProcessRemoteInput, TangoUpdate, ScriptRunDelayedStartupFrame,
				UpdateKinect, DeliverIosPlatformEvents, DispatchEventQueueEvents, DirectorSampleTime,
				PhysicsResetInterpolatedTransformPosition, SpriteAtlasManagerUpdate, PerformanceAnalyticsUpdate,
			};
		}

		public class FixedUpdate
		{
			public const string ClearLines = "FixedUpdate.ClearLines";
			public const string NewInputFixedUpdate = "FixedUpdate.NewInputFixedUpdate";
			public const string DirectorFixedSampleTime = "FixedUpdate.DirectorFixedSampleTime";
			public const string AudioFixedUpdate = "FixedUpdate.AudioFixedUpdate";
#if UNITY_2018_1_OR_NEWER
			public const string ScriptRunBehaviourFixedUpdate = "FixedUpdate.ScriptRunBehaviourFixedUpdate";
#else
		public const string ScriptRunBehaviourFixedUpdate = "FixedBehaviourUpdate";
#endif
			public const string DirectorFixedUpdate = "FixedUpdate.DirectorFixedUpdate";
			public const string LegacyFixedAnimationUpdate = "FixedUpdate.LegacyFixedAnimationUpdate";
			public const string XRFixedUpdate = "FixedUpdate.XRFixedUpdate";
			public const string PhysicsFixedUpdate = "FixedUpdate.PhysicsFixedUpdate";
			public const string Physics2DFixedUpdate = "FixedUpdate.Physics2DFixedUpdate";
			public const string DirectorFixedUpdatePostPhysics = "FixedUpdate.DirectorFixedUpdatePostPhysics";
			public const string ScriptRunDelayedFixedFrameRate = "FixedUpdate.ScriptRunDelayedFixedFrameRate";

			public static string[] TargetNames =
			{
				ClearLines, NewInputFixedUpdate, DirectorFixedSampleTime, AudioFixedUpdate, ScriptRunBehaviourFixedUpdate,
				DirectorFixedUpdate, LegacyFixedAnimationUpdate, XRFixedUpdate, PhysicsFixedUpdate, Physics2DFixedUpdate,
				DirectorFixedUpdatePostPhysics, ScriptRunDelayedFixedFrameRate,
			};
		}

		public class PreUpdate
		{
			public const string PhysicsUpdate = "PreUpdate.PhysicsUpdate";
			public const string Physics2DUpdate = "PreUpdate.Physics2DUpdate";
			public const string CheckTexFieldInput = "PreUpdate.CheckTexFieldInput";
			public const string IMGUISendQueuedEvents = "PreUpdate.IMGUISendQueuedEvents";
			public const string NewInputUpdate = "PreUpdate.NewInputUpdate";
			public const string SendMouseEvents = "PreUpdate.SendMouseEvents";
			public const string AIUpdate = "PreUpdate.AIUpdate";
			public const string WindUpdate = "PreUpdate.WindUpdate";
			public const string UpdateVideo = "PreUpdate.UpdateVideo";

			public static string[] TargetNames =
			{
				PhysicsUpdate, Physics2DUpdate, CheckTexFieldInput, IMGUISendQueuedEvents,
				NewInputUpdate, SendMouseEvents, AIUpdate, WindUpdate, UpdateVideo,
			};
		}

		public class Update
		{
#if UNITY_2018_1_OR_NEWER
			public const string ScriptRunBehaviourUpdate = "Update.ScriptRunBehaviourUpdate";
			public const string ScriptRunDelayedDynamicFrameRate = "Update.ScriptRunDelayedDynamicFrameRate";
#else
		public const string ScriptRunBehaviourUpdate = "BehaviourUpdate";
		public const string ScriptRunDelayedDynamicFrameRate = "CoroutinesDelayedCalls";
#endif
			public const string ScriptRunDelayedTasks = "Update.ScriptRunDelayedTasks";
			public const string DirectorUpdate = "Update.DirectorUpdate";

			public static string[] TargetNames =
			{
				ScriptRunBehaviourUpdate, ScriptRunDelayedDynamicFrameRate, ScriptRunDelayedTasks, DirectorUpdate,
			};
		}

		public class PreLateUpdate
		{
			public const string AIUpdatePostScript = "PreLateUpdate.AIUpdatePostScript";
			public const string DirectorUpdateAnimationBegin = "PreLateUpdate.DirectorUpdateAnimationBegin";
			public const string LegacyAnimationUpdate = "PreLateUpdate.LegacyAnimationUpdate";
			public const string DirectorUpdateAnimationEnd = "PreLateUpdate.DirectorUpdateAnimationEnd";
			public const string DirectorDeferredEvaluate = "PreLateUpdate.DirectorDeferredEvaluate";
			public const string EndGraphicsJobsAfterScriptUpdate = "PreLateUpdate.EndGraphicsJobsAfterScriptUpdate";
			public const string ParticleSystemBeginUpdateAll = "PreLateUpdate.ParticleSystemBeginUpdateAll";
#if UNITY_2018_1_OR_NEWER
			public const string ScriptRunBehaviourLateUpdate = "PreLateUpdate.ScriptRunBehaviourLateUpdate";
#else
		public const string ScriptRunBehaviourLateUpdate = "LateBehaviourUpdate";
#endif
			public const string ConstraintManagerUpdate = "PreLateUpdate.ConstraintManagerUpdate";

			public static string[] TargetNames =
			{
				AIUpdatePostScript, DirectorUpdateAnimationBegin, LegacyAnimationUpdate, DirectorUpdateAnimationEnd,
				DirectorDeferredEvaluate, EndGraphicsJobsAfterScriptUpdate, ParticleSystemBeginUpdateAll,
				ScriptRunBehaviourLateUpdate, ConstraintManagerUpdate,
			};
		}

		public class PostLateUpdate
		{
			public const string PlayerSendFrameStarted = "PostLateUpdate.PlayerSendFrameStarted";
			public const string DirectorLateUpdate = "PostLateUpdate.DirectorLateUpdate";
			public const string ScriptRunDelayedDynamicFrameRate = "PostLateUpdate.ScriptRunDelayedDynamicFrameRate";
			public const string PhysicsSkinnedClothBeginUpdate = "PostLateUpdate.PhysicsSkinnedClothBeginUpdate";
			public const string UpdateRectTransform = "PostLateUpdate.UpdateRectTransform";
			public const string UpdateCanvasRectTransform = "PostLateUpdate.UpdateCanvasRectTransform";
			public const string PlayerUpdateCanvases = "PostLateUpdate.PlayerUpdateCanvases";
			public const string UpdateAudio = "PostLateUpdate.UpdateAudio";
			public const string VFXUpdate = "PostLateUpdate.VFXUpdate";
			public const string ParticleSystemEndUpdateAll = "PostLateUpdate.ParticleSystemEndUpdateAll";
			public const string EndGraphicsJobsAfterScriptLateUpdate = "PostLateUpdate.EndGraphicsJobsAfterScriptLateUpdate";
			public const string UpdateCustomRenderTextures = "PostLateUpdate.UpdateCustomRenderTextures";
			public const string UpdateAllRenderers = "PostLateUpdate.UpdateAllRenderers";
			public const string EnlightenRuntimeUpdate = "PostLateUpdate.EnlightenRuntimeUpdate";
			public const string UpdateAllSkinnedMeshes = "PostLateUpdate.UpdateAllSkinnedMeshes";
			public const string ProcessWebSendMessages = "PostLateUpdate.ProcessWebSendMessages";
			public const string SortingGroupsUpdate = "PostLateUpdate.SortingGroupsUpdate";
			public const string UpdateVideoTextures = "PostLateUpdate.UpdateVideoTextures";
			public const string UpdateVideo = "PostLateUpdate.UpdateVideo";
			public const string DirectorRenderImage = "PostLateUpdate.DirectorRenderImage";
			public const string PlayerEmitCanvasGeometry = "PostLateUpdate.PlayerEmitCanvasGeometry";

			public const string PhysicsSkinnedClothFinishUpdate = "PostLateUpdate.PhysicsSkinnedClothFinishUpdate"; // Physics.UpdateCloth

			public const string FinishFrameRendering = "PostLateUpdate.FinishFrameRendering";

			// ここまで
			public const string BatchModeUpdate = "PostLateUpdate.BatchModeUpdate";
			public const string PlayerSendFrameComplete = "PostLateUpdate.PlayerSendFrameComplete";
			public const string UpdateCaptureScreenshot = "PostLateUpdate.UpdateCaptureScreenshot";
			public const string PresentAfterDraw = "PostLateUpdate.PresentAfterDraw";
			public const string ClearImmediateRenderers = "PostLateUpdate.ClearImmediateRenderers";
			public const string PlayerSendFramePostPresent = "PostLateUpdate.PlayerSendFramePostPresent";
			public const string UpdateResolution = "PostLateUpdate.UpdateResolution";
			public const string InputEndFrame = "PostLateUpdate.InputEndFrame";
			public const string TriggerEndOfFrameCallbacks = "PostLateUpdate.TriggerEndOfFrameCallbacks";
			public const string GUIClearEvents = "PostLateUpdate.GUIClearEvents";
			public const string ShaderHandleErrors = "PostLateUpdate.ShaderHandleErrors";
			public const string ResetInputAxis = "PostLateUpdate.ResetInputAxis";
			public const string ThreadedLoadingDebug = "PostLateUpdate.ThreadedLoadingDebug";
			public const string ProfilerSynchronizeStats = "PostLateUpdate.ProfilerSynchronizeStats";
			public const string MemoryFrameMaintenance = "PostLateUpdate.MemoryFrameMaintenance";
			public const string ExecuteGameCenterCallbacks = "PostLateUpdate.ExecuteGameCenterCallbacks";
			public const string ProfilerEndFrame = "PostLateUpdate.ProfilerEndFrame";

			public static string[] TargetNames =
			{
				PlayerSendFrameStarted, DirectorLateUpdate, ScriptRunDelayedDynamicFrameRate, PhysicsSkinnedClothBeginUpdate,
				UpdateRectTransform, UpdateCanvasRectTransform, PlayerUpdateCanvases, UpdateAudio, VFXUpdate,
				ParticleSystemEndUpdateAll,
				EndGraphicsJobsAfterScriptLateUpdate, UpdateCustomRenderTextures, UpdateAllRenderers, EnlightenRuntimeUpdate,
				UpdateAllSkinnedMeshes, ProcessWebSendMessages, SortingGroupsUpdate, UpdateVideoTextures, UpdateVideo,
				DirectorRenderImage,
				PlayerEmitCanvasGeometry, PhysicsSkinnedClothFinishUpdate, FinishFrameRendering,
			};
		}

		/// <summary>
		/// すべての要素名を取得する
		/// </summary>
		public static string[][] AllTargetNames =
		{
			Initialization.TargetNames,
			EarlyUpdate.TargetNames,
			FixedUpdate.TargetNames,
			PreUpdate.TargetNames,
			Update.TargetNames,
			PreLateUpdate.TargetNames,
			PostLateUpdate.TargetNames,
		};

		/// <summary>
		/// すべての要素数を取得する
		/// </summary>
		/// <returns></returns>
		public static int GetAllTargetLength()
		{
			return
				Initialization.TargetNames.Length +
				EarlyUpdate.TargetNames.Length +
				FixedUpdate.TargetNames.Length +
				PreUpdate.TargetNames.Length +
				Update.TargetNames.Length +
				PreLateUpdate.TargetNames.Length +
				PostLateUpdate.TargetNames.Length;
		}
	}
}
