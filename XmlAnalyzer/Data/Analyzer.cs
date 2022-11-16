﻿global using Resident = System.Collections.Generic.Dictionary<string, string>;

namespace XmlAnalyzer.Data
{
	public class Analyzer
	{
		public byte[] File { get; set; }
		public IAnalyzeStrategy AnalyzeStrategy { get; set; }

		public Analyzer() 
		{ 
			File = Array.Empty<byte>();
			AnalyzeStrategy = new LinqToXmlStrategy();
		}

		public Dictionary<string, Filter> GetOptions()
		{
			return AnalyzeStrategy.GetOptions(File);
		}

		public List<Resident> ParseXml(Dictionary<string, Filter>? filters = null)
		{
			return AnalyzeStrategy.ParseXml(File, filters);
		}
	}
}
