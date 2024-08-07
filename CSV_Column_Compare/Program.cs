namespace CSV_Column_Compare
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string referenceFile = "C:\\Users\\90258\\Downloads\\F.csv";
            string actualFile = "C:\\Users\\90258\\Downloads\\0Comp.1_amp.csv";

            var time = ExtractFileds<double>(referenceFile, 0);
            var xRef = ExtractFileds<double>(referenceFile, 1);
            var xAct = ExtractFileds<double>(actualFile, 1);

            var zipped = time.Zip(xRef, xAct);
            foreach ((double First, double Second, double Third) in zipped)
            {
                if (Math.Round(Second, 2) != Math.Round(Third, 2))
                {
                    Console.WriteLine($"{First} \t {Second} \t {Third}");
                }
            }
            Console.ReadKey();
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
