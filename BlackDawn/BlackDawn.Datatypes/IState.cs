using System;
namespace MMT.BlackDawn.Datatypes
{
	public interface IState
	{
		void Decrement();
		void Increment();
		void ZeroState();
		int GetState();
		void SetState( int state );
	}
}
