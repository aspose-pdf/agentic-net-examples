using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for page editing
using Aspose.Pdf;          // Contains alignment enums

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPath);

            // Specify that only page 3 should be processed (1‑based indexing)
            editor.ProcessPages = new int[] { 3 };

            // Align the original content vertically to the middle of the page
            // Correct property name and enum usage for Aspose.Pdf.Facades
            editor.VerticalAlignmentType = VerticalAlignment.Center; // middle vertical alignment

            // (Optional) you can also set horizontal alignment if needed
            // editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Save the modified PDF – no separate ApplyChanges call is required
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 vertically centered and saved to '{outputPath}'.");
    }
}
