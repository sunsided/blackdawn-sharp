using System;
using System.Collections.Generic;
using System.Text;

using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Triggers
{
	/// <summary>
	/// Determines what to do with the flag after TTL ends
	/// </summary>
	public enum TriggerFlagAction
	{
		/// <summary>
		/// After end of the TTL, the flag will be unset
		/// </summary>
		Unflag,

		/// <summary>
		/// After end of the TTL, the flag will be set
		/// </summary>
		Flag,

		/// <summary>
		/// After end of the TTL, the flag will be switched
		/// </summary>
		Switch
	}

	/// <summary>
	/// Determines what to do with the state after TTL ends
	/// </summary>
	public enum TriggerStateAction
	{
		/// <summary>
		/// After end of the TTL, the state will be incremented
		/// </summary>
		Increment,

		/// <summary>
		/// After end of the TTL, the state will be decremented
		/// </summary>
		Decrement,

		/// <summary>
		/// After end of the TTL, the state will be zeroed
		/// </summary>
		Zero
	}

	/// <summary>
	/// Determines the time warp affection
	/// </summary>
	public enum AffectionState
	{
		/// <summary>
		/// Trigger is affected by time warp effects
		/// </summary>
		Affected,

		/// <summary>
		/// Trigger is unaffected by time warp effects
		/// </summary>
		Unaffected
	}

	/// <summary>
	/// Determines the action after TTL end
	/// </summary>
	public enum ReactionMode
	{
		Terminate,
		Restart,
	}

	/// <summary>
	/// Delegate for watching the End of Life
	/// </summary>
	/// <param name="affectedFlag">The affected flag</param>
	/// <param name="sender">The sending trigger</param>
	public delegate void EndOfLifeFlagMonitor(IFlaggable affectedFlag, ITrigger sender);

	/// <summary>
	/// Delegate for watching the End of Life
	/// </summary>
	/// <param name="affectedState">The affected state</param>
	/// <param name="sender">The sending trigger</param>
	public delegate void EndOfLifeStateMonitor(IState affectedState, ITrigger sender);
}
