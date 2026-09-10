using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to edit page layout
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Target only page 2
            editor.ProcessPages = new int[] { 2 };

            // Center the original content horizontally on the result page
            // Use the non‑obsolete HorizontalAlignment enum (Aspose.Pdf.HorizontalAlignment)
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 content centered horizontally saved to '{outputPath}'.");
    }
}
