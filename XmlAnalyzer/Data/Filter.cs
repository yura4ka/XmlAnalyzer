namespace XmlAnalyzer.Data
{
	public class Filter : HashSet<string>
	{
		public Filter() : base() { }
		public Filter(HashSet<string> set) : base(set) { }
		public bool IsMatching(string value)
		{
			if (Count == 0)
				return true;

			return this.Any(v => v == value);
		}

		public static Comparer<string> CreateComparer(string name)
		{
			return Comparer<string>.Create((a, b) =>
			{
				return name != ResidentPropreties.BIRTHDAY ? a.CompareTo(b)
					: DateTime.ParseExact(a, "dd.MM.yyyy", null)
						.CompareTo(DateTime.ParseExact(b, "dd.MM.yyyy", null));
			});
		}
	}
}
