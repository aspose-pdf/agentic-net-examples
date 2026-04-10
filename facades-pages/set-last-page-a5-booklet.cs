using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        // Verify the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Get the total number of pages (1‑based indexing)
        int totalPages = pdfDocument.Pages.Count;
        if (totalPages == 0)
        {
            Console.Error.WriteLine("The PDF contains no pages.");
            return;
        }

        // Retrieve the last page
        Page lastPage = pdfDocument.Pages[totalPages];

        // Set the page size of the last page to A5 (compact booklet format)
        // Use PageInfo.Width / Height / IsLandscape instead of a non‑existent PageSize property.
        lastPage.PageInfo.Width = PageSize.A5.Width;
        lastPage.PageInfo.Height = PageSize.A5.Height;
        lastPage.PageInfo.IsLandscape = false; // A5 portrait

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Booklet PDF saved to '{outputPath}'.");
    }
}