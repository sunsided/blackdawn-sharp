using System;
using System.Collections.Generic;
using System.Text;

namespace MMT.BlackDawn.Optimizer
{
	/// <summary>
	/// Provides several optimization hints, controllable by the program
	/// </summary>
	public static class Optimizer
	{
		private static int _triggerCountInit = 20;

		/// <summary>
		/// The initial (expected) trigger load
		/// </summary>
		public static int TriggerCountInit
		{
			get { return Optimizer._triggerCountInit; }
			set { Optimizer._triggerCountInit = value; }
		}

		private static int _autoPulseMS = 20;

		/// <summary>
		/// The autopulse time in milliseconds
		/// </summary>
		public static int AutoPulseMilliseconds
		{
			get { return Optimizer._autoPulseMS; }
			set { Optimizer._autoPulseMS = value; }
		}

	}
}
