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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance (creation rule)
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Enumerate each table found
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                var table = absorber.TableList[i];
                Console.WriteLine($"Table {i + 1}: Page {table.PageNum}, Rectangle {table.Rectangle}");

                // Iterate rows
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate cells
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Concatenate text fragments within the cell
                        string cellText = "";
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        Console.WriteLine($"  Row {r + 1}, Cell {c + 1}: \"{cellText}\"");
                    }
                }
            }

            // Save the (potentially modified) document (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}