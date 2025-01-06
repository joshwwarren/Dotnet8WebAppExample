namespace JoshWWarren
{
    public class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public static class SoftwareManager
    {
        public static IEnumerable<Software> GetAllSoftware()
        {
            return
            [
                new Software
                {
                    Name = "MS Word",
                    Version = "13.2.1",
                },
                new Software
                {
                    Name = "AngularJS",
                    Version = "1.7.1",
                },
                new Software
                {
                    Name = "Angular",
                    Version = "13",
                },
                new Software
                {
                    Name = "React",
                    Version = "0.0.5",
                },
                new Software
                {
                    Name = "Vue.js",
                    Version = "2.6",
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "17.0.31919.166.0",
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "16.11.9.3.55",
                },
                new Software
                {
                    Name = "Visual Studio Code",
                    Version = "1.63",
                },
                new Software
                {
                    Name = "Blazor",
                    Version = "3.2.0",
                },
            ];
        }

        public static string Search(string? input)
        {
            var inputParts = input?.Split('.');

            if ((!inputParts?.All(a => int.TryParse(a, out _)) ?? true) || inputParts.Length > 5)
                return "<span style='color:red'>version is not valid</span>";

            var inputs = RegularizeNumberOfVersionSegments(inputParts);

            var results = new List<SoftwareSearch>();

            var softwareOptions = GetAllSoftware()
                .Select(w => new SoftwareSearch
                {
                    Name = w.Name,
                    Version = w.Version,
                    VersionArray = RegularizeNumberOfVersionSegments(w.Version.Split('.')),
                });

            foreach (var software in softwareOptions)
            {
                var i = 0;
                foreach (var input1 in inputs)
                {
                    var softwareVersion = software.VersionArray[i];

                    if (softwareVersion < input1)
                        break;
                    if (softwareVersion > input1)
                    {
                        results.Add(software);
                        break;
                    }

                    i++;
                }
            }

            return string.Join("<br/>", results.Select(s => $"{s.Name} {s.Version}"));
        }

        private static List<int> RegularizeNumberOfVersionSegments(IEnumerable<string> inputParts)
        {
            var versionSegments = inputParts.Select(s => Convert.ToInt32(s)).ToList();

            if (versionSegments.Count < 5)
                versionSegments = [.. versionSegments, .. Enumerable.Repeat(0, 4 - versionSegments.Count)];

            return versionSegments;
        }

        public class SoftwareSearch : Software
        {
            public List<int> VersionArray { get; set; }
        }
    }
}