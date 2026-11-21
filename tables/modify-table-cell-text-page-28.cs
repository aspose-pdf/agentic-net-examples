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

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 28 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 28)
            {
                Console.Error.WriteLine("The document contains fewer than 28 pages.");
                return;
            }

            // Create a TableAbsorber and visit page 28
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[28]);

            // If tables were found, modify the text of the first cell of the first table
            if (absorber.TableList.Count > 0)
            {
                // Get the first extracted table
                AbsorbedTable table = absorber.TableList[0];

                // Ensure the table has at least one row and one cell
                if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                {
                    var cell = table.RowList[0].CellList[0];
                    Console.WriteLine("Original cell fragments:");
                    foreach (var frag in cell.TextFragments)
                        Console.WriteLine($"  {frag.Text}");

                    // Modify the first text fragment of the cell (or all fragments if desired)
                    if (cell.TextFragments.Count > 0)
                    {
                        cell.TextFragments[0].Text = "Modified Text";
                        Console.WriteLine($"Modified fragment text: {cell.TextFragments[0].Text}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No tables were detected on page 28.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
