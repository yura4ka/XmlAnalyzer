namespace XmlAnalyzer.Data
{
	public class ResidentPropreties
	{
		public const string ROOT = "Residents";
		public const string PERSON = "Person";
		public const string NAME = "Name";
		public const string BIRTHDAY = "Birthday";
		public const string EMAIL = "Email";
		public const string FACULTY = "Faculty";
		public const string DEPARTMENT = "Department";
		public const string COURSE = "Course";
		public const string ADDRESS = "Address";
		public const string ROOM = "Room";

		public static string[] GetPropreties()
		{
			return new[] {
				NAME, 
				BIRTHDAY, 
				EMAIL, 
				FACULTY,
				DEPARTMENT,
				COURSE, 
				ADDRESS, 
				ROOM,
			};
		}

		public static Resident CreateResident()
		{
			var propreties = GetPropreties();
			var resident = new Resident();
			foreach (var p in propreties)
				resident[p] = "";
			return resident;
		}
	}
}
