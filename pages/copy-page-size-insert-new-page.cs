using System;
using System.IO;
using Aspose.Pdf;

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

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Verify that page 2 exists
                if (doc.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The document must contain at least two pages.");
                    return;
                }

                // Retrieve dimensions from page 2 (1‑based indexing)
                Page sourcePage = doc.Pages[2];
                double width = sourcePage.PageInfo.Width;
                double height = sourcePage.PageInfo.Height;

                // Insert a new empty page at position 5
                // If the document has fewer than 5 pages, the new page is appended at the end
                Page newPage = doc.Pages.Insert(5);

                // Apply the same size as page 2
                newPage.SetPageSize(width, height);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}