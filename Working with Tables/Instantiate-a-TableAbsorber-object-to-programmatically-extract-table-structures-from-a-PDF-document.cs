using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Instantiate TableAbsorber to search for tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Example: iterate over found tables and output cell text
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} found on page {table.PageNum}");

                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        foreach (var fragment in cell.TextFragments)
                        {
                            Console.WriteLine($"  Cell[{r},{c}] text: {fragment.Text}");
                        }
                    }
                }
            }

            // Save the (unchanged) document; PDF format does not require explicit SaveOptions
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}