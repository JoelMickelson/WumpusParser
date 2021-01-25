using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;


namespace WumpusParser
{
	public class APoint
	{


		private Point point;


		public bool HasPoint = false;


		public APoint()
		{
			point = new Point(0, 0);
		}

		public APoint(int x, int y)
		{
			point = new Point(x, y);
			HasPoint = true;
		}

		public APoint(APoint aPoint)
		{
			if (aPoint == null)
				return;

			if (aPoint.HasPoint == false)
			{
				point = new Point(0, 0);
				return;
			}

			point = new Point(aPoint.X, aPoint.Y);
		}

		//	private int _x;
		//	private int _y;


		public int X
		{
			get { if (HasPoint == false) return 0; return point.X; }
			set { point.X = value; HasPoint = true; }
		}

		public int Y
		{
			get { if (HasPoint == false) return 0; return point.Y; }
			set { point.Y = value; HasPoint = true; }
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			APoint other = obj as APoint;
			if ((System.Object)other == null)
			{
				return false;
			}

			if (this.X != other.X)
				return false;

			if (this.Y != other.Y)
				return false;

			if (this.HasPoint != other.HasPoint)
				return false;




			return true;
		}

        public override int GetHashCode()
        {
			return this.X + this.Y * 100;
        }


    }
}
