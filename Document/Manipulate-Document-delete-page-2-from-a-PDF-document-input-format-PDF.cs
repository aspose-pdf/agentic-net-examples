using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Ensure the document has at least two pages before attempting deletion
            if (pdfDocument.Pages.Count < 2)
            {
                Console.Error.WriteLine("Error: The document does not contain a second page to delete.");
                return;
            }

            // Delete page number 2 (PageCollection uses 1‑based indexing)
            pdfDocument.Pages.Delete(2);

            // Save the modified document (using the provided document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Page 2 successfully removed. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}