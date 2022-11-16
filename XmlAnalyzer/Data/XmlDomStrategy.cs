using System.Xml;

namespace XmlAnalyzer.Data
{
	public class XmlDomStrategy : IAnalyzeStrategy
	{
		public Dictionary<string, Filter> GetOptions(byte[] file)
		{
			var result = new Dictionary<string, Filter>();
			var xml = new XmlDocument();
			xml.Load(new MemoryStream(file));
			var names = ResidentPropreties.GetPropreties();

			foreach (string name in names)
			{
				var nodes = xml.SelectNodes($"//{name}");
				var filter = new HashSet<string>();

				if (nodes == null)
				{
					result[name] = new Filter(filter);
					continue;
				}

				var comparer = Filter.CreateComparer(name);
				foreach (XmlNode node in nodes)
					filter.Add(node.InnerText);
				result[name] = new Filter(filter.OrderBy(o => o, comparer).ToHashSet());
			}

			return result;
		}

		public List<Resident> ParseXml(byte[] file, Dictionary<string, Filter>? filters)
		{
			var result = new List<Resident>();
			var xml = new XmlDocument();
			xml.Load(new MemoryStream(file));
			var root = xml.SelectSingleNode(ResidentPropreties.ROOT);
			var people = root?.SelectNodes($"descendant::{ResidentPropreties.PERSON}");
			var names = ResidentPropreties.GetPropreties();

			if (people == null)
				return result;

			foreach (XmlNode node in people)
			{
				var resident = ResidentPropreties.CreateResident();
				bool isFilterMatched = true;
				foreach (string name in names)
				{
					var value = node.SelectSingleNode($"descendant::{name}")?.InnerText ?? "";
					resident[name] = value;
					isFilterMatched = filters?[name].IsMatching(value) ?? true;
					if (!isFilterMatched)
						break;
				}
				if (isFilterMatched)
					result.Add(resident);
			}

			return result;
		}
	}
}
