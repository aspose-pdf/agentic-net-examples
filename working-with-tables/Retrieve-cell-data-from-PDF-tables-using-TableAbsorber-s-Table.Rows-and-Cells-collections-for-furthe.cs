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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document (Visit(Document))
            absorber.Visit(doc);

            // Iterate over each detected table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} found on page {table.PageNum}");

                // Iterate over rows in the table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate over cells in the row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Concatenate all text fragments within the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        // Example processing: output cell position and its text
                        Console.WriteLine($"  Row {r + 1}, Cell {c + 1}: \"{cellText}\"");
                    }
                }
            }

            // Save the (potentially modified) document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}