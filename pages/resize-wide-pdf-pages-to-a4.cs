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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double pageWidth = page.PageInfo.Width;

                // Resize only pages wider than 600 points to A4 size
                if (pageWidth > 600)
                {
                    // Set page size to A4 (width and height in points)
                    page.SetPageSize(Aspose.Pdf.PageSize.A4.Width, Aspose.Pdf.PageSize.A4.Height);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}