using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageToDelete = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the overload that accepts a file path.
            Document pdfDoc = new Document(inputPath);

            // Validate the requested page number.
            if (pageToDelete < 1 || pageToDelete > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageToDelete} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Delete the specified page.
            pdfDoc.Pages.Delete(pageToDelete);

            // Save the modified PDF.
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Page {pageToDelete} deleted. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
