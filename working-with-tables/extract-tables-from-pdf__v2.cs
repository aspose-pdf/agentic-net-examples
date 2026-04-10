using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from all pages of the document
            absorber.Visit(doc);

            // Iterate through each discovered table and output its cell contents
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} on page {table.PageNum}:");

                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        // Combine all text fragments within the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        Console.Write($"[{cellText}]");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            // Save the original PDF (layout remains unchanged)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}