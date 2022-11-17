using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace XmlAnalyzer.Data
{
	public class LinqToXmlStrategy : IAnalyzeStrategy
	{
		public string GetName() => "LINQ to XML";
		public Dictionary<string, Filter> GetOptions(byte[] file)
		{
			var xml = XDocument.Load(new MemoryStream(file));
			var result = new Dictionary<string, Filter>();
			var names = ResidentPropreties.GetPropreties();
			var people = xml.Descendants(ResidentPropreties.PERSON);

			foreach (string name in names)
			{
				var comparer = Filter.CreateComparer(name);
				result[name] = new Filter(people
					.Select(p => p.Element(name)?.Value ?? "")
					.OrderBy(o => o, comparer)
					.ToHashSet());
			}


			return result;
		}

		public List<Resident> ParseXml(byte[] file, Dictionary<string, Filter>? filters)
		{
			var xml = XDocument.Load(new MemoryStream(file));
			var names = ResidentPropreties.GetPropreties();
			var people = xml.Descendants(ResidentPropreties.PERSON);
			List<Resident> result = new(people.Count());
			foreach (var p in people)
			{
				var resident = ResidentPropreties.CreateResident();
				bool isFilterMatched = true;
				foreach (string name in names)
				{
					string value = p.Element(name)?.Value ?? "";
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
