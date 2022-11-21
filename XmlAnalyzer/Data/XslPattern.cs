namespace XmlAnalyzer.Data
{
    public static class XslPattern
    {
		public const string file = @"
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" version=""1.0"">
<xsl:template match=""Residents"">
<html>
	<head>
		<meta charset=""utf-8"" />
	<style>
		table, th, td { border: 1px solid black; }
		table { border-collapse: collapse; }
		td, th { padding: 5px; }
	</style>
	</head>
	<body>
		<table>
			<tr>
				<th>ПІБ</th>
				<th>День народження</th>
				<th>Електронна адреса</th>
				<th>Факультет</th>
				<th>Кафедра</th>
				<th>Курс</th>
				<th>Адреса гуртожитку</th>
				<th>Кімната</th>
			</tr>
			<xsl:for-each select=""Person"">
				<tr>
					<td><xsl:value-of select=""Name"" /></td>
					<td><xsl:value-of select=""Birthday"" /></td>
					<td><xsl:value-of select=""Email"" /></td>
					<td><xsl:value-of select=""Faculty"" /></td>
					<td><xsl:value-of select=""Department"" /></td>
					<td><xsl:value-of select=""Course"" /></td>
					<td><xsl:value-of select=""Address"" /></td>
					<td><xsl:value-of select=""Room"" /></td>
				</tr>
			</xsl:for-each>
		</table>
	</body>
</html>
</xsl:template>
</xsl:stylesheet>";
    }
}
