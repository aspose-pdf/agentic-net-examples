using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to modify page sizes.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Specify the pages to be processed (3 through 6, 1‑based indexing).
            editor.ProcessPages = new int[] { 3, 4, 5, 6 };

            // Set the desired output page size to US Letter.
            editor.PageSize = PageSize.PageLetter;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 resized to Letter and saved as '{outputPath}'.");
    }
}