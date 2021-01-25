using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;


namespace WumpusParser
{
	[Serializable()]
	public class Room
	{
		public List<Character> Residents = new List<Character>();

		public Point Position;
		public Dungeon MyDungeon;

		public bool ExploredByPlayer = false;


		public bool HasWumpus;
		public bool HasPit;
		public bool HasStairs;
		public bool HasGold;
		public bool HasWumpusCorpse;
		//public bool 

		private List<Room> GetAllAdjacentRooms()
		{
			List<Room> rooms = new List<Room>();

			for (int i=Position.X - 1; i<= Position.X + 1; i++)
			{
				for (int j=Position.Y - 1; j <= Position.Y + 1; j++)
				{
					if (i == Position.X && j == Position.Y)
						continue;

					Room r = MyDungeon.GetRoom(i, j);
					if (r != null)
						rooms.Add(r);
				}
			}


			return rooms;

		}

		private List<Room> GetAllOrthoAdjacentRooms()
		{
			List<Room> rooms = new List<Room>();

			for (int i = Position.X - 1; i <= Position.X + 1; i++)
			{
				Room r = MyDungeon.GetRoom(i, Position.Y);
				if (r != null)
					rooms.Add(r);
			}


			for (int j = Position.Y - 1; j <= Position.Y + 1; j++)
			{
				Room r = MyDungeon.GetRoom(Position.X, j);
				if (r != null)
					rooms.Add(r);
			}

			rooms.Remove(this);
			rooms.Remove(this);

			return rooms;

		}


		internal bool HasStench()
		{
			List<Room> adjacentRooms = GetAllOrthoAdjacentRooms();

			foreach (Room r in adjacentRooms)
			{
				if (r.HasWumpus)
					return true;
			}

			return false;
		}

		

		internal bool HasBreeze()
		{
			List<Room> adjacentRooms = GetAllOrthoAdjacentRooms();

			foreach (Room r in adjacentRooms)
			{
				if (r.HasPit)
					return true;
			}

			return false;
		}

		internal bool HasExit()
		{

			if (this.HasStairs)
				return true;

			return false;
		}

		internal bool HasGlitter()
		{

			if (this.HasGold)
				return true;
	

			return false;
		}

	}
}
