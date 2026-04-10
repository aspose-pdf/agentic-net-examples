using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to modify page sizes.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Specify the pages to be processed (1‑based indexing).
            editor.ProcessPages = new int[] { 3, 4, 5, 6 };

            // Set the desired page size – US Letter (279 x 216 points).
            editor.PageSize = PageSize.PageLetter;

            // Apply the changes to the selected pages.
            editor.ApplyChanges();

            // Save the modified document.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 resized to Letter and saved as '{outputPath}'.");
    }
}