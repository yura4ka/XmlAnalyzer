global using Resident = System.Collections.Generic.Dictionary<string, string>;
using System.Xml.Xsl;
using System.Xml;

namespace XmlAnalyzer.Data
{
	public class Analyzer
	{
		public byte[] File { get; private set; }
		public IAnalyzeStrategy AnalyzeStrategy { get; set; }
		public string Name { get; private set; }

		public Analyzer() 
		{ 
			File = Array.Empty<byte>();
			AnalyzeStrategy = new LinqToXmlStrategy();
			Name = string.Empty;
		}

		public void SetFile(string name, byte[] file)
		{
			Name = name;
			File = file;
		}

		public Dictionary<string, Filter> GetOptions()
		{
			return AnalyzeStrategy.GetOptions(File);
		}

		public List<Resident> ParseXml(Dictionary<string, Filter>? filters = null)
		{
			return AnalyzeStrategy.ParseXml(File, filters);
		}

		public Stream TransformToHTML()
		{
			var xslt = new XslCompiledTransform();
			xslt.Load(new XmlTextReader(new StringReader(XslPattern.file)));
			var resultStream = new MemoryStream(); 
			var result = XmlWriter.Create(resultStream);
			var input = new MemoryStream(File);
			xslt.Transform(new XmlTextReader(input), result);
			result.Flush();
			resultStream.Position = 0;
			return resultStream;
		}
	}
}
