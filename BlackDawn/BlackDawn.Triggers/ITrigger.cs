using System;
namespace MMT.BlackDawn.Triggers
{
	public interface ITrigger : IPulseable
	{
		void FastForward();
		// void Pulse(long millisecElapsed);
		void Start();
		void Suspend();
		bool Running { get; }
		object Tag { get; set; }
	}
}
