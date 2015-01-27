using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MMT.BlackDawn.Triggers
{
	/// <summary>
	/// The global timer manager
	/// </summary>
	public static class LocalTimerManager
	{

		private static System.Timers.Timer autotimer = null;

		/// <summary>
		/// Enables autopulsing in a given timeframe
		/// </summary>
		/// <param name="ms">The milliseconds between pulsing</param>
		public static void EnableAutoPulse(long ms)
		{
			if (autotimer != null) autotimer.Dispose();
			autotimer = new System.Timers.Timer(Convert.ToDouble(ms));
			autotimer.AutoReset = true;
			autotimer.Elapsed += new System.Timers.ElapsedEventHandler(autotimer_Elapsed);
			prepulsed = false;
			autotimer.Start();
		}

		/// <summary>
		/// Disables autopulsing
		/// </summary>
		public static void DisableAutoPulse()
		{
			if (autotimer != null) autotimer.Dispose();
		}

		/// <summary>
		/// Gets whether autopulsing is enabled or not
		/// </summary>
		public static bool AutoPulseEnabled
		{
			get { return autotimer != null; }
		}

		private static bool prepulsed = false;

		private static void autotimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (!prepulsed)
			{
				prepulsed = true;
				return;
			}
			Pulse();
		}

		private static List<ITrigger> _affected = new List<ITrigger>(BlackDawn.Optimizer.Optimizer.TriggerCountInit);
		private static List<ITrigger> _unaffected = new List<ITrigger>(BlackDawn.Optimizer.Optimizer.TriggerCountInit);

		/// <summary>
		/// Registeres a trigger from the list
		/// </summary>
		/// <param name="trigger">The trigger to be added</param>
		/// <param name="affection">Set to Affected if the trigger is affected by local time effects</param>
		public static void RegisterTrigger(ITrigger trigger, AffectionState affection)
		{
			if (affection == AffectionState.Affected)
			{
				if (!_affected.Contains(trigger))
				{
					_affected.Add(trigger);
					//Console.WriteLine("Added af-trigger, " + _affected.Count.ToString() + " now in list.");
				}
			}
			else
			{
				if (!_unaffected.Contains(trigger))
				{
					_unaffected.Add(trigger);
					//Console.WriteLine("Added unaf-trigger, " + _unaffected.Count.ToString() + " now in list.");
				}
			}
		}

		///// <summary>
		///// Unregisteres a trigger from the list
		///// </summary>
		///// <param name="trigger">The trigger to be removed</param>
		//public static void UnregisterTrigger(ITrigger trigger)
		//{
		//    _affected.Remove(trigger);
		//    _unaffected.Remove(trigger);
		//}

		/// <summary>
		/// Unregisteres a trigger from the list
		/// </summary>
		/// <param name="trigger">The trigger to be removed</param>
		public static void UnregisterTrigger(ITrigger trigger, AffectionState affection)
		{
			if (affection == AffectionState.Affected)
			{
				_affected.Remove(trigger);
				//Console.WriteLine("Removed af-trigger, " + _affected.Count.ToString() + " remaining in list.");
			}
			else
			{
				_unaffected.Remove(trigger);
				//Console.WriteLine("Removed unaf-trigger, " + _unaffected.Count.ToString() + " remaining in list.");
			}
		}

		/// <summary>
		/// The timestamp of the last pulse
		/// </summary>
		private static long lastAffectedPulse = System.DateTime.Now.Ticks;

		/// <summary>
		/// The timestamp of the last pulse
		/// </summary>
		private static long lastUnaffectedPulse = System.DateTime.Now.Ticks;

		/// <summary>
		/// Initializes the timer to the current time
		/// </summary>
		public static void InitializeToNow()
		{
			lastUnaffectedPulse = System.DateTime.Now.Ticks;
		}

		private static Thread Unaffected, Affected;

		/// <summary>
		/// Pulses the GTM
		/// </summary>
		public static void Pulse()
		{
			if (_unaffected.Count != 0)
			{
				if (Unaffected != null) Unaffected.Join();
				Unaffected = new Thread(new ThreadStart(PulseUnaffected));
				Unaffected.Start();
			}

			if (_affected.Count != 0)
			{
				if (Affected != null) Affected.Join();
				Affected = new Thread(new ThreadStart(PulseAffected));
				Affected.Start();
			}
		}

		/// <summary>
		/// Pulses the GTM
		/// </summary>
		public static void PulseUnthreaded()
		{
			PulseUnaffected();
			PulseAffected();
		}

		/// <summary>
		/// Pulses the unaffected timers
		/// </summary>
		private static void PulseUnaffected()
		{
			// calculate
			long currentPulseTime = System.DateTime.Now.Ticks;
			long elapsed = currentPulseTime - lastUnaffectedPulse;
			lastUnaffectedPulse = currentPulseTime;

			// loop over affected triggers now
			lock (_unaffected)
			{
				ITrigger[] array = _unaffected.ToArray();
				foreach (ITrigger trigger in array)
				{
					if (trigger.Running)
					{
						//TODO: should be asychronous
						trigger.Pulse(elapsed);
					}
				}
			}
		}

		/// <summary>
		/// Pulses the affected timers
		/// </summary>
		private static void PulseAffected()
		{
			// calculate
			long currentPulseTime = System.DateTime.Now.Ticks;
			long elapsed = currentPulseTime - lastAffectedPulse;
			lastAffectedPulse = currentPulseTime;

			// loop over affected triggers now
			lock (_affected)
			{
				ITrigger[] array = _affected.ToArray();
				foreach (ITrigger trigger in array)
				{
					if (trigger.Running)
					{
						//TODO: should be asychronous
						trigger.Pulse(elapsed);
					}
				}
			}
		}
	}
}
