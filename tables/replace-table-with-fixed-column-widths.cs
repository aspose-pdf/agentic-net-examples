using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber resides here

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 14 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 14)
            {
                Console.Error.WriteLine("Document does not contain page 14.");
                return;
            }

            // Get page 14
            Page page = doc.Pages[14];

            // Absorb tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // Iterate over each detected table and replace it with a new one that has explicit column widths
            foreach (AbsorbedTable oldTable in absorber.TableList)
            {
                // Preserve the original table bounds (LLX, LLY, URX, URY)
                var rect = oldTable.Rectangle;

                // Create a new table with explicit column widths
                Table newTable = new Table
                {
                    ColumnWidths = "100 100 100",
                    // Position the new table at the same location as the old one using MarginInfo
                    // MarginInfo(left, bottom, right, top)
                    Margin = new MarginInfo((float)rect.LLX, (float)rect.LLY, 0, 0)
                };

                // Add a single row with placeholder cells (adjust as needed)
                Row row = newTable.Rows.Add();
                row.Cells.Add("Cell 1");
                row.Cells.Add("Cell 2");
                row.Cells.Add("Cell 3");

                // Replace the old absorbed table with the new one on the same page
                absorber.Replace(page, oldTable, newTable);
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
