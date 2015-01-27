using System;
using System.Collections.Generic;
using System.Text;

namespace MMT.BlackDawn.Triggers
{
	public interface IPulseable
	{
		void Pulse(long msElapsed);
	}
}
