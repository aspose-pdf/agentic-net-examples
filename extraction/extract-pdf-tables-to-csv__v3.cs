using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "tables.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Extract tables from all pages using the core TableAbsorber API
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(doc); // correct method – not Accept()

            // Write CSV file
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // CSV header
                writer.WriteLine("PageNumber,RectLLX,RectLLY,RectURX,RectURY,RowCount");

                // Iterate over extracted tables (AbsorbedTable objects)
                foreach (AbsorbedTable absorbedTable in tableAbsorber.TableList)
                {
                    int pageNumber = absorbedTable.PageNum; // 1‑based page index
                    Aspose.Pdf.Rectangle rect = absorbedTable.Rectangle;
                    int rowCount = absorbedTable.RowList?.Count ?? 0;

                    writer.WriteLine($"{pageNumber},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY},{rowCount}");
                }
            }
        }

        Console.WriteLine($"CSV file created at '{csvPath}'.");
    }
}
