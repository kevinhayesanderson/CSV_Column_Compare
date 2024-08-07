namespace CSV_Column_Compare
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\Users\\90258\\source\\repos\\CSV_Column_Compare\\";
            string referenceFile = "C:\\Users\\90258\\Downloads\\F.csv";

            string actualFile = "C:\\Users\\90258\\Downloads\\0Comp.1_amp.csv";

            var time = ExtractFileds<double>(referenceFile, 0);

            var xRef = ExtractFileds<double>(referenceFile, 1);
            var xAct = ExtractFileds<double>(actualFile, 1);

            actualFile = "C:\\Users\\90258\\Downloads\\1Comp.2_amp.csv";

            var yRef = ExtractFileds<double>(referenceFile, 2);
            var yAct = ExtractFileds<double>(actualFile, 1);


            var file = path + "xRef==xAct.csv";
            Console.WriteLine(file);
            var zipped = time.Zip(xRef, xAct);
            compareAndExportDiff(zipped, file);

            file = path + "yRef==yAct.csv";
            Console.WriteLine(file);
            zipped = time.Zip(yRef, yAct);
            compareAndExportDiff(zipped, file);

            Console.ReadKey();

            static void compareAndExportDiff(IEnumerable<(double First, double Second, double Third)> zipped, string file = default)
            {
                StreamWriter fileStream = default;
                if (file != default) fileStream = new StreamWriter(file);

                foreach ((double First, double Second, double Third) in zipped)
                {
                    if (Math.Round(Second, 2) != Math.Round(Third, 2))
                    {
                        string line = $"{First} \t {Second} \t {Third}";
                        Console.WriteLine(line);
                        fileStream?.WriteLine(line);
                    }
                }
                fileStream?.Close();
            }
        }
        static List<T> ExtractFileds<T>(string file, int column)
        {
            List<T> result = [];
            using (StreamReader sr = new(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    result.Add((T)Convert.ChangeType(fields[column], typeof(T)));
                }
            }
            return result;
        }
    }
}
