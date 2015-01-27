using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Initializing...");
			Python.Runtime.PythonEngine.Initialize();

			Console.WriteLine("Going Main...");
			Python.Runtime.Runtime.Py_Main(1, new string[1]);

			Console.ReadKey();
			Console.WriteLine("Finalizing...");
			Python.Runtime.PythonEngine.Shutdown();

		}
	}
}
