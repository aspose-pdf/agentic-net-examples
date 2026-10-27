using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document, change the size of the last page to A5, and save.
        using (Document doc = new Document(inputPath))
        {
            // 1‑based page indexing – get the last page.
            int lastPageNumber = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageNumber];

            // Set the page dimensions to A5.
            // Aspose.Pdf.PageSize provides static A5 dimensions (Width & Height are in points).
            lastPage.PageInfo.Width = PageSize.A5.Width;
            lastPage.PageInfo.Height = PageSize.A5.Height;
            // Optional: adjust orientation flag if needed.
            lastPage.PageInfo.IsLandscape = lastPage.PageInfo.Width > lastPage.PageInfo.Height;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Last page set to A5 and saved as '{outputPath}'.");
    }
}
