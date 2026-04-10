using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to modify page appearance
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Target only page 4
            editor.ProcessPages = new int[] { 4 };

            // Set horizontal alignment (center) for the original content on the result page
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Increase zoom to 150% for better readability
            editor.Zoom = 1.5f;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}