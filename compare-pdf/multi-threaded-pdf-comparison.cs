using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Define PDF pairs to compare (left, right, result)
        var pairs = new (string left, string right, string result)[]
        {
            ("doc1_a.pdf", "doc1_b.pdf", "result1.pdf"),
            ("doc2_a.pdf", "doc2_b.pdf", "result2.pdf")
        };

        var tasks = new Task[pairs.Length];
        for (int i = 0; i < pairs.Length; i++)
        {
            var pair = pairs[i];
            tasks[i] = Task.Run(() =>
            {
                if (!File.Exists(pair.left) || !File.Exists(pair.right))
                {
                    Console.Error.WriteLine($"Missing files for pair: {pair.left}, {pair.right}");
                    return;
                }

                using (Document doc1 = new Document(pair.left))
                using (Document doc2 = new Document(pair.right))
                {
                    var options = new SideBySideComparisonOptions();
                    SideBySidePdfComparer.Compare(doc1, doc2, pair.result, options);
                    Console.WriteLine($"Comparison saved to {pair.result}");
                }
            });
        }

        Task.WaitAll(tasks);
        Console.WriteLine("All comparisons completed.");
    }
}