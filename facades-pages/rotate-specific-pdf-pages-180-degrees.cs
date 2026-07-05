using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor from Aspose.Pdf.Facades to rotate specific pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Define rotations: pages 3, 5, and 7 will be rotated by 180 degrees
            editor.PageRotations = new System.Collections.Generic.Dictionary<int, int>
            {
                { 3, 180 },
                { 5, 180 },
                { 7, 180 }
            };

            // Apply the rotation changes
            editor.ApplyChanges();

            // Save the resulting PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}