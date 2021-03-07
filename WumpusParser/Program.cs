using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusParser
{
	class Program
	{

		static Game game = new Game();

		static void Main(string[] args)
		{



			// select mode

			// create dungeon

			// create and place PC
			while (true)
			{
				game.Startup();
				game.Run();

				game = new Game();

				Console.WriteLine("\n\n");
				Console.WriteLine("Restarting game. Press control-C to exit.");
				Console.WriteLine("\n\n");
				

			}


		//	GenerateDungeon();

			//Play();

		}

		private static void Play()
		{
			while (true)
			{
				// display map
				// display sensorium

				// get input
				Console.WriteLine("put in a command");
				string command = Console.ReadLine();
			}
		}

		private static void GenerateDungeon()
		{
		//	throw new NotImplementedException();
		}






	}
}
