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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Append a new empty page at the end of the document
            Page newPage = doc.Pages.Add();

            // Set the page size to A4 (210 mm × 297 mm). Aspose.Pdf uses points.
            newPage.PageInfo.Width = PageSize.A4.Width;
            newPage.PageInfo.Height = PageSize.A4.Height;

            // Save the modified document while the Document instance is still alive
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with an extra A4 page: {outputPath}");
    }
}