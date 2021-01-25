using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Drawing;


namespace WumpusParser
{
	public partial class Character : GameObject
	{
		private bool actionClimb(Actuation actionIntent)
		{
			if (this.CurrentRoom.HasExit() == false)
			{
				Game.Announce("There is no way to climb here.");
				return false;
			}

			// !!!! assuming that only player will exit



			this.Score += 1000;

			Game.SetState("gameover");

			return true;
		}

		private bool actionShoot(Actuation actionIntent)
		{
			if (actionIntent.Direction == null)
			{
				Game.Announce("The direction was incorrectly set!");
				return false;
			}


			if (this.ArrowsRemaining < 1)
			{
				Game.Announce("You have no more arrows!");
				return false;
			}


			APoint d = Language.ConvertSDirectionToPoint(actionIntent.Direction);

			// in this version, arrow only kills wumpus if it was adjacent

			int tx = X + d.X;
			int ty = Y + d.Y;

			Room targetRoom = this.CurrentDungeon.GetRoom(tx, ty);


			Score--;
			ArrowsRemaining--;


			if (targetRoom == null)
			{
				Game.Announce("The arrow slams into a wall and shatters.");
				return true;
			}


			Game.Announce("The arrow flies off into the darkness.");

			while (targetRoom != null)
			{
				if (targetRoom.HasWumpus)
				{
					targetRoom.HasWumpus = false;
					targetRoom.HasWumpusCorpse = true;

					Game.Announce("You hear a pained scream that echos throughout the dungeon!");
					Game.KilledWumpus = true;

					Score += 1000;


					// add proper percept



					return true;
				}

				tx += d.X;
				ty += d.Y;
				targetRoom = CurrentDungeon.GetRoom(tx, ty);

			}




			return true;




		}

		private bool actionTake(Actuation actionIntent)
		{
			if (this.CurrentRoom.HasGold)
			{
				CurrentRoom.HasGold = false;

				Game.Announce("Smiling, you slip the gold into your pocket.");
				Game.GotGold = true;

				Score += 1000;

				return true;
			}


			Game.Announce("There is nothing here that you can take.");
			return false;
		}

		private bool actionGo(Actuation actionIntent)
		{
			APoint d = Language.ConvertSDirectionToPoint(actionIntent.Direction);

			int tx = X + d.X;
			int ty = Y + d.Y;

			Room targetRoom = this.CurrentDungeon.GetRoom(tx, ty);

			if (targetRoom == null)
			{
				Game.Announce("Ouch! You bump into a wall.");

				// !!! do proper percept

				return true;
			}


			CurrentRoom.Residents.Remove(this);
			targetRoom.Residents.Add(this);
			this.X = targetRoom.Position.X;
			this.Y = targetRoom.Position.Y;
			this.CurrentRoom = targetRoom;

			this.CurrentRoom.ExploredByPlayer = true;
			Score--;

			Game.NeedToSeeMap = true;

			if (targetRoom.HasWumpus)
			{
				Game.Announce("The hungry wumpus quickly eats you.");
				Score -= 1000;
				this.Alive = false;
				Game.SetState("gameover");
				return true;

			}

			if (targetRoom.HasPit)
			{
				Game.Announce("You fall into a pit and die.");
				Score -= 1000;
				this.Alive = false;
				Game.SetState("gameover");
				return true;
			}

			return true;
		}

	}
}
