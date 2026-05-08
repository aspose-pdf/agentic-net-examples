using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber and AbsorbedTable live here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a fresh TableAbsorber for the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Loop through each detected table and output its rectangle coordinates
                for (int i = 0; i < absorber.TableList.Count; i++)
                {
                    AbsorbedTable table = absorber.TableList[i];
                    Aspose.Pdf.Rectangle rect = table.Rectangle;

                    // Output the page number and rectangle bounds (llx, lly, urx, ury)
                    Console.WriteLine($"Page {page.Number}: Table {i + 1} – " +
                                      $"LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}");
                }
            }
        }
    }
}
