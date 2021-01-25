using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusParser
{
	public class GameObject
	{
		public List<Trait> Traits = new List<Trait>();

		public bool HasTrait(string s)
		{
			return (Traits.Find(o => o.Label == s) != null);
		}
	}
}
