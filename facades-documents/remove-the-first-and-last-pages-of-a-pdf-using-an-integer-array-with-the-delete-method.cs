using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Determine the total number of pages in the source PDF
        int totalPages;
        using (Document doc = new Document(inputPath)) // Document lifecycle: create, load, save (dispose)
        {
            totalPages = doc.Pages.Count;
        }

        // Prepare the page numbers to delete: first page (1) and last page (totalPages)
        int[] pagesToDelete = new int[] { 1, totalPages };

        // Use PdfFileEditor to delete the specified pages and save the result
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (success)
        {
            Console.WriteLine($"Successfully removed first and last pages. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages from the PDF.");
        }
    }
}