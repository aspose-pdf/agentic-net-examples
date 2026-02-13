using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Example: delete page 2 and page 4 (1‑based indexing)
        // Ensure the pages exist before attempting deletion
        int[] pagesToDelete = { 2, 4 };
        foreach (int pageNum in pagesToDelete)
        {
            if (pageNum >= 1 && pageNum <= pdfDocument.Pages.Count)
            {
                pdfDocument.Pages.Delete(pageNum);
                // After a deletion, page numbers shift, so adjust subsequent indices
                // Decrement remaining page numbers greater than the deleted one
                for (int i = 0; i < pagesToDelete.Length; i++)
                {
                    if (pagesToDelete[i] > pageNum)
                        pagesToDelete[i]--;
                }
            }
            else
            {
                Console.WriteLine($"Warning: Page {pageNum} does not exist and cannot be deleted.");
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Pages deleted successfully. Output saved to {outputPath}");
    }
}