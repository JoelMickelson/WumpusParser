using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WumpusParser
{
	[Serializable()]
	public class Dungeon
	{
		public Room[,] Rooms;

		public int Width;
		public int Height;

		

		public Dungeon(int x, int y)
		{
			Width = x;
			Height = y;

			Rooms = new Room[Width, Height];

			for (int i=0; i<Width; i++)
			{
				for (int j=0; j<Height; j++)
				{
					Rooms[i, j] = new Room();
					Rooms[i, j].Position = new Point(i, j);
					Rooms[i, j].MyDungeon = this;
				}
			}
		}

		/// <summary>
		/// turns it into a wumpus dungeon, featuring the titular wumpus and some pits
		/// will not place either next to the stairs
		/// the wumpus is further restricted and will not be kitty corner from the stairs
		/// </summary>
		/// <param name="nPits"></param>
		public void Wumpusize(int nPits = 3)
		{
			GetRoom(0, 0).HasStairs = true;

			Random r = new Random();

			for (int i=0; i<nPits; i++)
			{
				for (int attempt=0; attempt<100; attempt++)
				{
					int x = r.Next(Width);
					int y = r.Next(Height);

					Room room = this.GetRoom(x, y);

					if (room.HasStairs || room.HasWumpus || room.HasPit || room.HasGold)
						continue;

					if (x == 1 ^ y == 1)
						continue;

					room.HasPit = true;
					break;
				}
			}


			for (int attempt = 0; attempt < 100; attempt++)
			{
				int x = r.Next(Width);
				int y = r.Next(Height);

				Room room = this.GetRoom(x, y);

				if (room.HasStairs || room.HasWumpus || room.HasPit || room.HasGold)
					continue;

				if (x == 1 || y == 1)
					continue;


				room.HasWumpus = true;
				break;
			}

			for (int attempt = 0; attempt < 100; attempt++)
			{
				int x = r.Next(Width);
				int y = r.Next(Height);

				Room room = this.GetRoom(x, y);

				if (room.HasStairs || room.HasWumpus || room.HasPit)
					continue;

				room.HasGold = true;
				break;
			}
			
		}


		internal Room GetRoom(int x, int y)
		{
			if (x < 0 || y < 0)
				return null;

			if (x >= Width || y >= Height)
				return null;

			return this.Rooms[x, y];
		}
	}
}
