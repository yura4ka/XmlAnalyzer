﻿namespace XmlAnalyzer.Data
{
	public interface IAnalyzeStrategy
	{
		public Dictionary<string, Filter> GetOptions(byte[] file);
		public List<Resident> ParseXml(byte[] file, Dictionary<string, Filter>? filters);
	}
}
