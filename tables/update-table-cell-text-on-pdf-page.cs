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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 16 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 16)
            {
                Console.Error.WriteLine("The document does not contain page 16.");
                return;
            }

            // Get page 16
            Page pageSixteen = doc.Pages[16];

            // Create a TableAbsorber and visit the page to extract tables
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(pageSixteen);

            // Check that at least one table was found
            if (tableAbsorber.TableList.Count == 0)
            {
                Console.Error.WriteLine("No tables were detected on page 16.");
                return;
            }

            // Work with the first detected table
            AbsorbedTable absorbedTable = tableAbsorber.TableList[0];

            // Ensure the table has at least one row and one column
            if (absorbedTable.RowList.Count == 0 || absorbedTable.RowList[0].CellList.Count == 0)
            {
                Console.Error.WriteLine("The detected table is empty.");
                return;
            }

            // Change the text of the cell at row 1, column 1 (0‑based indices)
            AbsorbedCell targetCell = absorbedTable.RowList[0].CellList[0];
            // Remove any existing fragments and add a new one with the desired text
            targetCell.TextFragments.Clear();
            targetCell.TextFragments.Add(new TextFragment("Updated cell text"));

            // Save the modified PDF back to disk (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated table cell to '{outputPath}'.");
    }
}
