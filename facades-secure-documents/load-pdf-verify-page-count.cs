using System;
using Aspose.Pdf; // Use the main Aspose.Pdf namespace for Document class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using the Document class (the primary API for Aspose.Pdf)
            using (Document pdfDoc = new Document(inputPath))
            {
                // Verify successful loading by checking the page count
                int pageCount = pdfDoc.Pages.Count;
                Console.WriteLine($"PDF loaded successfully. Page count: {pageCount}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading PDF: {ex.Message}");
        }
    }
}
