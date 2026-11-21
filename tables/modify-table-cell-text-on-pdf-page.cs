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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least five pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document contains fewer than 5 pages.");
                return;
            }

            // Create a TableAbsorber and visit page 5 to extract tables
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[5]);

            // If a table was found, modify its first cell's text
            if (absorber.TableList.Count > 0)
            {
                AbsorbedTable table = absorber.TableList[0];

                if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                {
                    var cell = table.RowList[0].CellList[0];
                    // Change the text of the first TextFragment in the cell, if any
                    if (cell.TextFragments.Count > 0)
                    {
                        cell.TextFragments[0].Text = "Updated cell text";
                    }
                    else
                    {
                        // If the cell has no fragments, add a new one
                        cell.TextFragments.Add(new TextFragment("Updated cell text"));
                    }
                }
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
