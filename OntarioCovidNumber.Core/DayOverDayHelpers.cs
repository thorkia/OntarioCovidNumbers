using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OntarioCovidNumber.Core
{
	public static class DayOverDayHelpers
	{
		internal static decimal PercentChange(int today, int previousDay)
		{
			return PercentChange((decimal) today, (decimal) previousDay);
		}

		internal static decimal PercentChange(decimal today, decimal previousDay)
		{
			if (today == 0 && previousDay == 0)
			{
				return 0m;
			}

			if (today == 0)
			{
				return -100m;
			}

			if (previousDay == 0)
			{
				return 100m;
			}

			return Math.Abs(today / previousDay - 1.0m) * 100;
		}

		public static ChangeDirection GetChangeDirection(int today, int previousDay)
		{
			return GetChangeDirection((decimal) today, (decimal) previousDay);
		}

		public static ChangeDirection GetChangeDirection(decimal today, decimal previousDay)
		{
			if (today < previousDay)
			{
				return ChangeDirection.Decrease;
			}
			else if (today > previousDay)
			{
				return ChangeDirection.Increase;
			}


			return ChangeDirection.NoChange;
		}

		public static ChangeType GetChangeType(string field, ChangeDirection direction, ISet<string> increaseGoodFields)
		{
			switch (direction)
			{
				case ChangeDirection.Increase:
					if (increaseGoodFields.Contains(field.ToUpper()))
					{
						return ChangeType.Better;
					}

					return ChangeType.Worse;
				case ChangeDirection.Decrease:
					if (increaseGoodFields.Contains(field.ToUpper()))
					{
						return ChangeType.Worse;
					}

					return ChangeType.Better;
				case ChangeDirection.NoChange:
				default:
					return ChangeType.NoChange;

			}
		}
	}
}
