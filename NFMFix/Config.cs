﻿using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace NoteMovementFix

{
	internal class Config
	{
		public static Config Instance;
		public virtual bool Enabled { get; set; } = false;
		public virtual bool InstantRotation { get; set; } = false;
		public virtual bool InstantSwap { get; set; } = false;
		public virtual bool DisableCloseRotation { get; set; } = false;
		public virtual bool HiddenFloorMovement { get; set; } = false;
		public virtual bool DisableNJS { get; set; } = false;
		public virtual bool FakeGhostMode { get; set; } = false;
		public virtual bool FakeGhostNote { get; set; } = true;
		public virtual bool FakeGhostArrow { get; set; } = true;
		public virtual float Layer { get; set; } = 6;
		public virtual bool RemoveHMDPause { get; set; } = false;
		public virtual bool DisablePause { get; set; } = false;

		/// <summary>
		/// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
		/// </summary>
		public virtual void OnReload()
		{
			// Do stuff after config is read from disk.
			if (DisableCloseRotation)
			{
				Plugin.Submission = false;
				BS_Utils.Gameplay.ScoreSubmission.ProlongedDisableSubmission("NoteMovementFix");
			}
		}

		/// <summary>
		/// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
		/// </summary>
		public virtual void Changed()
		{
			// Do stuff when the config is changed.
			if(Plugin.Submission && (DisableCloseRotation || InstantSwap))
			{
				Plugin.Submission = false;
				BS_Utils.Gameplay.ScoreSubmission.ProlongedDisableSubmission("NoteMovementFix");
			}
			else if(!Plugin.Submission && !DisableCloseRotation && !InstantSwap)
			{
				Plugin.Submission = true;
				BS_Utils.Gameplay.ScoreSubmission.RemoveProlongedDisable("NoteMovementFix");
			}
		}

		/// <summary>
		/// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
		/// </summary>
		public virtual void CopyFrom(Config other)
		{
			// This instance's members populated from other
		}
	}
}
