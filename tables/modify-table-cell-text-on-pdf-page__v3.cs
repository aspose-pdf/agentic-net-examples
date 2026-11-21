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

        // Open the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document contains at least 25 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 25)
            {
                Console.Error.WriteLine("Document has fewer than 25 pages.");
                return;
            }

            // Retrieve page 25
            Page page25 = doc.Pages[25];

            // Use TableAbsorber to extract tables from the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page25);

            // If a table was found, modify the text of a specific cell
            if (absorber.TableList.Count > 0)
            {
                // Take the first absorbed table (you can iterate if multiple tables exist)
                AbsorbedTable table = absorber.TableList[0];

                // Ensure the table has at least one row and one column
                if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                {
                    var cell = table.RowList[0].CellList[0];
                    // Replace the existing text fragments with a new one
                    cell.TextFragments.Clear();
                    cell.TextFragments.Add(new TextFragment("Updated cell text"));
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
