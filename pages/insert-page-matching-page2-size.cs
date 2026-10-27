using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least two pages (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Retrieve the PageInfo (size, margins, etc.) from page 2
            PageInfo sourceInfo = doc.Pages[2].PageInfo;

            // Insert a new blank page at position 5 (1‑based index)
            // If the document has fewer than 4 pages, Insert adds the page at the end
            int insertPosition = 5;
            doc.Pages.Insert(insertPosition);

            // Get the newly inserted page and copy the size/margin information from the source page
            Page newPage = doc.Pages[insertPosition];
            newPage.PageInfo.Width  = sourceInfo.Width;
            newPage.PageInfo.Height = sourceInfo.Height;
            // Copy margin information if needed (MarginInfo is a reference type, so we clone its values)
            if (sourceInfo.Margin != null)
            {
                newPage.PageInfo.Margin = new MarginInfo(
                    sourceInfo.Margin.Left,
                    sourceInfo.Margin.Right,
                    sourceInfo.Margin.Top,
                    sourceInfo.Margin.Bottom);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted a new page at position 5. Saved to '{outputPath}'.");
    }
}
