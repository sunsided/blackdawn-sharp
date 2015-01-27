using System;
namespace MMT.BlackDawn.Datatypes
{
	public interface IFlaggable
	{
		/// <summary>
		/// Flags the value
		/// </summary>
		void Flag();

		/// <summary>
		/// Returns whether the Flag is set or not
		/// </summary>
		bool Flagged { get; }

		/// <summary>
		/// Unflags the value
		/// </summary>
		void Unflag();

		/// <summary>
		/// Switches the flag's state
		/// </summary>
		void SwitchFlag();

	}
}
