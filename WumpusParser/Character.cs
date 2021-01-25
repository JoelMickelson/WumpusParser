using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusParser
{
	public partial class Character : GameObject
	{
		public int X;
		public int Y;
		public Dungeon CurrentDungeon;
		public Room CurrentRoom;


		public int ArrowsRemaining = 1;

		public int Score = 0;

		public bool Alive = true;


		internal void Place(Dungeon targetDungeon, int x, int y)
		{
			X = x;
			Y = y;

			CurrentDungeon = targetDungeon;

			CurrentRoom = CurrentDungeon.GetRoom(x, y);

			

		}

		/*internal void DoAction(Actuation a)
		{
			
		}*/
	}
}
