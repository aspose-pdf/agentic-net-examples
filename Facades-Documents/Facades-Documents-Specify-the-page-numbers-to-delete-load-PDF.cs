using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Page numbers to delete (1‑based indexing)
        int[] pagesToDelete = { 2, 4 };

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade and bind the PDF document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                // Delete pages in descending order to avoid index shifting
                Array.Sort(pagesToDelete);
                for (int i = pagesToDelete.Length - 1; i >= 0; i--)
                {
                    int pageNumber = pagesToDelete[i];

                    // Ensure the page number is within the current page count
                    if (pageNumber >= 1 && pageNumber <= editor.Document.Pages.Count)
                    {
                        // Delete the specified page
                        editor.Document.Pages.Delete(pageNumber);
                        Console.WriteLine($"Deleted page {pageNumber}");
                    }
                    else
                    {
                        Console.WriteLine($"Skipped page {pageNumber}: out of range.");
                    }
                }

                // Save the modified PDF
                editor.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}