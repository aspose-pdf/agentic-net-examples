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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Append a new empty page at the end of the document
            Page newPage = doc.Pages.Add();

            // Set the page size to A4 (210 mm × 297 mm).
            // Aspose.Pdf provides the dimensions in points via PageSize.A4.
            newPage.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
            // Alternative approach (equivalent):
            // newPage.PageInfo.Width = PageSize.A4.Width;
            // newPage.PageInfo.Height = PageSize.A4.Height;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"A4 page appended and saved to '{outputPath}'.");
    }
}
