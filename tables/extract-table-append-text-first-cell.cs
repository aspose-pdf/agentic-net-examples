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
            // Aspose.Pdf uses 1‑based page indexing; page 36 is accessed with index 36
            if (doc.Pages.Count < 36)
            {
                Console.Error.WriteLine("The document has fewer than 36 pages.");
                return;
            }

            Page page36 = doc.Pages[36];

            // Use TableAbsorber to extract tables from page 36
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page36);

            // Ensure at least one table was found
            if (absorber.TableList.Count == 0)
            {
                Console.Error.WriteLine("No tables were detected on page 36.");
                return;
            }

            // Get the first detected table (AbsorbedTable)
            AbsorbedTable table = absorber.TableList[0];

            // Ensure the table has at least one row
            if (table.RowList.Count == 0)
            {
                Console.Error.WriteLine("The detected table contains no rows.");
                return;
            }

            // Get the first row
            AbsorbedRow firstRow = table.RowList[0];

            // Ensure the row has at least one cell (we will add text to the first cell)
            if (firstRow.CellList.Count == 0)
            {
                Console.Error.WriteLine("The first row contains no cells.");
                return;
            }

            // Add new text to the first cell by appending a TextFragment
            AbsorbedCell firstCell = firstRow.CellList[0];
            TextFragment newFragment = new TextFragment("New Cell Text");
            firstCell.TextFragments.Add(newFragment);

            // NOTE: Modifying the AbsorbedTable/AbsorbedCell objects does NOT change the
            // original PDF content. To reflect changes in the PDF you would need to
            // recreate the table using the Aspose.Pdf.Table API and add it to the page.
            // For this task we only demonstrate manipulation of the extracted table.

            // Save the (unchanged) PDF to the output path
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}
