using System.Xml;

namespace XmlAnalyzer.Data
{
	public class SaxStrategy : IAnalyzeStrategy
	{
		public Dictionary<string, Filter> GetOptions(byte[] file)
		{
			var xml = new XmlTextReader(new MemoryStream(file));
			var result = new Dictionary<string, Filter>();
			string[] names = ResidentPropreties.GetPropreties();
			Array.ForEach(names, name => result[name] = new Filter());

			xml.ReadToFollowing(ResidentPropreties.ROOT);
			while (xml.Read())
			{
				switch (xml.NodeType)
				{
					case XmlNodeType.Element:
						string name = xml.Name;
						int i = Array.IndexOf(names, name);
						if (i != -1)
							result[name].Add(xml.ReadInnerXml());
						break;
				}
			}

			foreach (var name in result.Keys)
			{
				var comparer = Filter.CreateComparer(name);
				result[name] = new Filter(result[name].OrderBy(o => o, comparer).ToHashSet());
			}

			return result;
		}

		public List<Resident> ParseXml(byte[] file, Dictionary<string, Filter>? filters)
		{
			var xml = new XmlTextReader(new MemoryStream(file));
			string[] names = ResidentPropreties.GetPropreties();
			var result = new List<Resident>();
			Resident current = ResidentPropreties.CreateResident();

			xml.ReadToFollowing(ResidentPropreties.ROOT);
			bool isFilterMatched = true;
			while (xml.Read())
			{
				switch (xml.NodeType)
				{
					case XmlNodeType.Element:
						if (!isFilterMatched)
							continue;
						string name = xml.Name;
						int i = Array.IndexOf(names, name);
						if (i == -1)
							break;
						var value = xml.ReadInnerXml();
						if (!filters?[name].IsMatching(value) ?? false)
						{
							current = ResidentPropreties.CreateResident();
							isFilterMatched = false;
							continue;
						}
						current[name] = value;
						break;
					case XmlNodeType.EndElement:
						if (xml.Name == ResidentPropreties.PERSON)
						{
							if(isFilterMatched)
								result.Add(current);
							current = ResidentPropreties.CreateResident();
							isFilterMatched = true;
						}
						break;
				}
			}

			return result;
		}
	}
}
