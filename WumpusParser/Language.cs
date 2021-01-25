using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WumpusParser
{
	public static class Language
	{



		internal static string ConvertToDirection(string p)
		{
			p = p.ToLower();

			if (p == "west" || p == "w")
				return "west";

			if (p == "east" || p == "e")
				return "east";

			if (p == "north" || p == "n")
				return "north";

			if (p == "south" || p == "s")
				return "south";


			return null;
		}

		internal static APoint ConvertSDirectionToPoint(string p)
		{
			p = p.ToLower();

			if (p == "west")
				return new APoint(-1, 0);
			if (p == "east")
				return new APoint(1, 0);
			if (p == "north")
				return new APoint(0, -1);
			if (p == "south")
				return new APoint(0, 1);

			return null;
		}

	} // end of class
}
