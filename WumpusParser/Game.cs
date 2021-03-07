using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusParser
{
	public class Game
	{

		public Dungeon MainDungeon;

		public Character Player;


		private bool fullView = false;


	//	public static string state = "normal";

		public enum GameState
		{
			Unknown,
			Human,
			AIWithBreak,
			AIFull,
			GameOver,
			Exit
		}

		private static GameState state = GameState.Unknown;

		public static bool NeedToSeeMap = true;

		public static bool KilledWumpus = false;
		public static bool GotGold = false;

		public static void SetState(string s)
		{
			if (s == "gameover")
				state = GameState.GameOver;
		}

		public void Startup()
		{
	//		MainDungeon = new Dungeon(5, 5);
		//	MainDungeon.Wumpusize();

			Player = new Character();

		//	Player.Place(MainDungeon, 0, 0);
		//	MainDungeon.GetRoom(0, 0).ExploredByPlayer = true;
		}

		public void Run()
		{

	//		DoQuickTest();

			// get the various settings for the dungeon and gameplay
			DoGameIntro();


			Announce("You enter the dungeon, looking for treasure.");
			
			int numPits = 0;
			foreach (Room r in MainDungeon.Rooms)
			{
				if (r.HasPit)
					numPits++;
			}

			string ps = string.Format("Beware, this place is home to {0} pits, and the dreaded wumpus!", numPits);
			Announce(ps);


			
			//Announce("Beware, this place is home to " )


			while (true)
			{
				Console.WriteLine("\nScore: " + Player.Score);
				string l = string.Format("Position: {0},{1}", Player.X, Player.Y);
				Console.WriteLine(l);
				// display map

				if (Game.NeedToSeeMap)
					DisplayMap();

				NeedToSeeMap = false;


				// display sensorium
				Room r = Player.CurrentRoom;
				if (r.HasStench())
				{
					Console.WriteLine("\nYou smell an awful stench here.");
				}

				if (r.HasBreeze())
				{
					Console.WriteLine("\nYou feel a breeze coming from somewhere.");
				}

				if (r.HasExit())
				{
					Console.WriteLine("\nThere is an exit to the dungeon here.");
				}
				
				if (r.HasGold)
				{
					Console.WriteLine("\nYou see the glitter of gold.");
				}

				if (r.HasWumpusCorpse)
				{
					Console.WriteLine("\nThere is a wumpus corpse here.");
				}


			//	Console.Write("\n> ");
				
				// get input
			//	Console.WriteLine("put in a command");
				
				if (state == GameState.Human)
				{
					Console.Write("\n> ");
					DoPlayerCommand();
				}
				else if (state == GameState.AIWithBreak)
				{
					Console.WriteLine("\npress any key to continue...");
				//	Console.Write("\n> ");
					Console.ReadKey(true);

					DoAICommand();
				}
				else if (state == GameState.AIFull)
				{
					Console.Write("\n> ");
					DoAICommand();
				}
				
				

				if (state == GameState.GameOver)
				{
					break;
				}

				if (state == GameState.Exit)
				{
					break;
				}



			}

			if (state == GameState.GameOver)
			{
				Console.WriteLine("Your adventure has ended.");

				string s = "You scored " + Player.Score + " points.";
				Console.WriteLine(s);

				if (GotGold && Player.Alive)
				{
					s = "You managed to find and retrieve the dungeon's treasure.";
					Console.WriteLine(s);
				}

				if (KilledWumpus)
				{
					s = "You killed the wumpus!";
					Console.WriteLine(s);
				}

				// show the entire map now that the game is over
				DisplayMap(true);

				Console.ReadKey();

			}


		}

	
		private void DoPlayerCommand()
		{
			string command = Console.ReadLine();

			Actuation a = WInterpretCommand(command);

			if (a != null)
			{
				Player.DoAction(a);
			}
		}


		private void DoAICommand()
		{
			// really simple hack command to get the infrastructure working
			string command = "south";

			// TODO: put actual AI code here



			//
			
			Console.WriteLine(command);

			// alternatively, and as orginally envisioned, AI systems could present an Actuation object
			// by presenting command strings, AI agents are explicitly taking the role of a human player, rather than as a character in the game world

			// this way, the AI agent depends on the same exact parser that the player does
			// on the other hand, the AI now needs to have a way to articulate its commands to the parser
			Actuation a = WInterpretCommand(command);


			if (a != null)
			{
				Player.DoAction(a);
			}
		}

		private void DoGameIntro()
		{
			

			Announce("Greetings, adventurer! Welcome to the custom dungeon. You may choose your dungeon settings before going in. Isn't that convenient?");
			Announce("\nFirst, how big should the dungeon be? [5]");

			string big = Console.ReadLine();

			int size;
			if (!int.TryParse(big, out size))
				size = 5;


		
//			float nominalPitDensity = 3.0f / (5.0f * 5.0f);
	//		int area = size * size;

		//	float fdefaultPitCount = (area * nominalPitDensity) + 0.5f;
			//int defaultPitCount = (int)fdefaultPitCount;


			float nominalPitDensity = 3.0f / (5.0f);
			float fdefaultPitCount = (size * nominalPitDensity) + 0.5f;


			int defaultPitCount = 3;
			int extraSpace = size * size - 25;
			extraSpace /= 5;

		
			defaultPitCount += extraSpace;



			
			float npd2 = 3.0f / 25.0f;
			float fdp2 = (size * size) * npd2;
			defaultPitCount = (int)(fdp2 + 0.5);
			
			
			
		//	int defaultPitCount = (int)fdefaultPitCount;

			
			Announce("How many traps would you like there to be? [" + defaultPitCount + "]");

			int actualPitCount;
			if (!int.TryParse(Console.ReadLine(), out actualPitCount))
				actualPitCount = defaultPitCount;




			MainDungeon = new Dungeon(size, size);
			MainDungeon.Wumpusize(actualPitCount);




			Player.Place(MainDungeon, 0, 0);
			MainDungeon.GetRoom(0, 0).ExploredByPlayer = true;

			state = GameState.Human;


			// game style selection, in which either the human player is in charge or an AI system


            //Announce("How would you like the game to be played?");
            //Announce("1: by human");
            //Announce("2: by AI with breaks in between");
            //Announce("3: by AI as fast as possible");

            //int gameStyle = 1;
            //if (int.TryParse(Console.ReadLine(), out gameStyle))
            //{
            //    if (gameStyle < 1 || gameStyle > 3)
            //    {
            //        gameStyle = 1;
            //    }
            //}
            //else
            //    gameStyle = 1;

            //switch (gameStyle)
            //{
            //    case 1:
            //        state = GameState.Human;
            //        break;
            //    case 2:
            //        state = GameState.AIWithBreak;
            //        break;
            //    case 3:
            //    default:
            //        state = GameState.AIFull;
            //        break;
            //}

            return;
        }



        /// <summary>
        /// this is just a really quick minimalist parser for something that has a really low, uncomplicated action set
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private Actuation WInterpretCommand(string command)
		{
			// climb
			// take [gold]
			// shoot arrow {direction}
			// go {direction}
			// shortcut n s e w

			Actuation a;


			string[] textToxens = command.Split(' ');

			if (textToxens.Length == 0)
				return null;

			string verb = textToxens[0].ToLower();

			//if (verb == "exit" || verb == "quit")
			if (verb == "quit")
			{
				state = GameState.Exit;
				return null;
			}


			if (verb == "look")
			{
				NeedToSeeMap = true;
				return null;
			}

			if (verb == "climb" || verb == "leave" || verb == "exit")
			{
				a = new Actuation();
				a.Verb = "climb";
				return a;
			}

			if (verb == "take" || verb == "grab" || verb == "get")
			{
				a = new Actuation();
				a.Verb = "take";
				return a;
			}


			if (verb == "shoot")
			{
				a = new Actuation();
				
				if (textToxens.Length < 2)
				{
					Announce("You don't know where to shoot.");
					return null;
				}

				// direction is either 2nd or 3rd word
				string direction = Language.ConvertToDirection(textToxens[1]);

				if (direction == null && textToxens.Length > 2)
				{
					direction = Language.ConvertToDirection(textToxens[2]);
				}

				if (direction == null)
				{
					Announce("You can't tell which direction to shoot.");
					return null;
				}
				
				
				a.Verb = "shoot";
				a.Direction = direction;

				return a;
			}

			if (verb == "go")
			{
				a = new Actuation();

				if (textToxens.Length < 2)
				{
					Announce("You don't know where to go.");
					return null;
				}

				// direction is either 2nd or 3rd word
				string direction = Language.ConvertToDirection(textToxens[1]);

				if (direction == null && textToxens.Length > 2)
				{
					direction = Language.ConvertToDirection(textToxens[2]);
				}

				if (direction == null)
				{
					Announce("You can't tell which direction to go.");
					return null;
				}


				a.Verb = "go";
				a.Direction = direction;

				return a;
			}

			string d = Language.ConvertToDirection(textToxens[0]);
			if (d != null)
			{
				a = new Actuation();

				a.Verb = "go";
				a.Direction = d;

				return a;
			}


			Announce("Unknown command.");
			return null;

		}

		private static int lineLimit = 78;
		public static void Announce(string s)
		{
			//Console.WriteLine(s);
			//return;

			//Console.WriteLine("12345678901234567890123456789012345678901234567890123456789012345678901234567890");
			
			// split the string so that words don't get broken
			if (s.Length < lineLimit)
			{
				Console.WriteLine(s);
				return;
			}


			string[] words = s.Split(' ');
			StringBuilder builder = new StringBuilder();
			//bool toobig = false;

			for (int i = 0; i < words.Length; i++)
			{
				int newLength = builder.Length + words[i].Length + 1;

				// it would make us get too big, so let's not add that word
				if (newLength > lineLimit)
				{
					if (builder.Length == 0) // a really long single word
						Console.WriteLine(words[i]);
					else
					{
						i--;
						Console.WriteLine(builder);
						builder.Clear();
					}

				}
				else
				{
					builder.Append(words[i]);
					if (i != words.Length - 1 && builder.Length < lineLimit)
						builder.Append(' ');
				}

				if (i == words.Length - 1)
				{
					Console.WriteLine(builder);
					break;
				}
			}
		}

		private void DisplayMap(bool revealMap = false)
		{
			for (int y=0; y<MainDungeon.Height; y++)
			{
				string l;
				if (revealMap)
					l = PrintMapLine(y, MainDungeon, false, true);  // end of game view
				else
					l = PrintMapLine(y, MainDungeon); // typical during-play view
				Console.WriteLine(l);
			}
		}

		private string PrintMapLine(int y, Dungeon targetDungeon, bool showPlayer = true, bool showAll = false)
		{
			string s = "";

			for (int x=0; x<targetDungeon.Width; x++)
			{
				Room r = targetDungeon.GetRoom(x, y);
				if (r == null)
				{
					Console.WriteLine("problem in printmapline");
				}


				if (showPlayer)
				{
					if (r == Player.CurrentRoom)
					{
						s += '@';
						continue;
					}

				}
				
				if (r.ExploredByPlayer == false && !fullView && !showAll)
				{
					s += '?';
				}
				else if (r.HasWumpus)
				{
					s += 'W';
				}
				else if (r.HasPit)
				{
					s += '_';
				}
				else if (r.HasStairs)
				{
					s += '<';
				}
				else if (r.HasGold)
				{
					s += '$';
				}



				else
					s += '.';


			}

			return s;
		}



	} // end of class
}
