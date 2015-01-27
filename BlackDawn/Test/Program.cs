using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using MMT.BlackDawn;
using MMT.BlackDawn.Optimizer;
using MMT.BlackDawn.Triggers;
using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Tools.Test
{
	class Program
	{

		private static bool exiting = false;
		static void Looping()
		{
			Console.WriteLine("Entering threaded pulse loop");
			while (!exiting)
			{
				Triggers.LocalTimerManager.PulseUnthreaded();
			};
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Enabling LTM in AutoPulse mode");
			// LocalTimerManager.EnableAutoPulse(Optimizer.Optimizer.AutoPulseMilliseconds * 10);

			Thread pulse = new Thread(new ThreadStart(Looping));
			pulse.Start();

			Console.WriteLine("Creating flag");
			State flag = new State();
			Console.WriteLine("Misusing it as an autonomous triggered switch-flag");
			TriggeredFlag<State> flagtrigger = new TriggeredFlag<State>(flag, TriggeredFlag<State>.DurationOneSecondMS, TriggerFlagAction.Switch, AffectionState.Unaffected, ReactionMode.Restart);
			flagtrigger.Flag();
			flagtrigger.EndOfLifeArrived += new EndOfLifeFlagMonitor(flag_EndOfLifeArrived);

			Console.WriteLine("Creating state");
			State state = new State();
			Console.WriteLine("Misusing it as an autonomous triggered increment-state");
			TriggeredState<State> statetrigger = new TriggeredState<State>(state, TriggeredState<State>.DurationTenSecondsMS, TriggerStateAction.Increment, AffectionState.Unaffected, ReactionMode.Restart);
			statetrigger.SetState(0);
			statetrigger.EndOfLifeArrived += new EndOfLifeStateMonitor(statetrigger_EndOfLifeArrived);

			Console.WriteLine("Starting loop.");
			LocalTimerManager.InitializeToNow();
			flagtrigger.Start();
			statetrigger.Start();

			Console.ReadKey();
			exiting = true;
		}

		static void statetrigger_EndOfLifeArrived(IState affectedState, ITrigger sender)
		{
			Console.WriteLine("It is " + DateTime.Now.ToLongTimeString() + "@" + DateTime.Now.Ticks.ToString() + " and the state just switched to " + (affectedState.GetState()).ToString());
		}

		static void flag_EndOfLifeArrived(IFlaggable affectedFlag, ITrigger sender)
		{
			Console.WriteLine("It is " + DateTime.Now.ToLongTimeString() + "@" + DateTime.Now.Ticks.ToString() + " and the flag just switched to " + (affectedFlag.Flagged).ToString());
		}
	}
}
