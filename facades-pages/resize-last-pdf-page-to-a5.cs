using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Use PdfPageEditor (Facade) to modify the PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Get total number of pages (1‑based indexing)
            int totalPages = editor.GetPages();

            // Target only the last page
            editor.ProcessPages = new int[] { totalPages };

            // Set the output page size to A5
            editor.PageSize = PageSize.A5;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Last page resized to A5 and saved as '{outputPath}'.");
    }
}