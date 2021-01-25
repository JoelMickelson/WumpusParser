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
		public delegate bool GeneralActionFunction(Actuation actionIntent);


		Dictionary<string, GeneralActionFunction> ActionMappings = null;

		public void InitializeActionMappings()
		{
			ActionMappings = new Dictionary<string, GeneralActionFunction>();


			//ActionMappings.

			ActionMappings.Add("go", actionGo);
			ActionMappings.Add("take", actionTake);
			ActionMappings.Add("shoot", actionShoot);
			ActionMappings.Add("climb", actionClimb);

			// climb
			// take [gold]
			// shoot arrow {direction}
			// go {direction}
		
		
			//			ActionMappings.Add("createProject", actionCreateProject);
		}

		


		public bool DoAction(Actuation actionIntent)
		{

			if (ActionMappings == null)
				InitializeActionMappings();

			// playerInput and some AI can use PreviousAction to easily repeat things - we want this even on a failure
			//this.PreviousAction = actionIntent;



			if (ActionMappings.ContainsKey(actionIntent.Verb)) // we found it in the action dictionary
			{
				ActionMappings[actionIntent.Verb](actionIntent); // execute the action delegate
				//		Game.Announce("agent executing actionIntent: " + actionIntent.Verb);
				return true;
			}

			//Game.Announce("malformed command - verb not found in action mappings: " + actionIntent.Verb);



			return false;
		}

	}
}