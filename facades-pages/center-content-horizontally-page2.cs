using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor from Aspose.Pdf.Facades to modify page layout
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Apply changes only to page 2 (1‑based indexing)
            editor.ProcessPages = new int[] { 2 };

            // Center the original content horizontally on the result page
            // Use the non‑obsolete HorizontalAlignment enum (Aspose.Pdf.HorizontalAlignment)
            editor.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Apply the modifications
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Centered content on page 2 saved to '{outputPath}'.");
    }
}
