using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document API (Facades API does not expose page count or size setters)
        Document pdfDocument = new Document(inputPath);

        // Get total number of pages (1‑based indexing)
        int pageCount = pdfDocument.Pages.Count;

        // Resize the last page to A5 using PageInfo
        Page lastPage = pdfDocument.Pages[pageCount];
        lastPage.PageInfo.Width = PageSize.A5.Width;
        lastPage.PageInfo.Height = PageSize.A5.Height;

        // Save the updated PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Last page set to A5 and saved as '{outputPath}'.");
    }
}
