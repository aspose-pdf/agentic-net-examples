using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF
        const string inputPath = "input.pdf";
        // Path for the resulting PDF
        const string outputPath = "output.pdf";

        // Example: pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 4 };

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Create the facade and bind the PDF
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                // Delete pages in descending order to avoid index shifting
                Array.Sort(pagesToDelete);
                for (int i = pagesToDelete.Length - 1; i >= 0; i--)
                {
                    int pageNumber = pagesToDelete[i];
                    if (pageNumber >= 1 && pageNumber <= editor.Document.Pages.Count)
                    {
                        editor.Document.Pages.Delete(pageNumber);
                    }
                }

                // Save the modified document
                editor.Save(outputPath);
            }

            Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}