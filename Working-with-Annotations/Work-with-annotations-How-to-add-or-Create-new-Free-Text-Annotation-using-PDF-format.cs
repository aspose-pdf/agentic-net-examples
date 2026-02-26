using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_freetext.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade and bind the existing PDF.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) using the fully qualified
            // System.Drawing.Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a free‑text annotation on page 1 with the specified contents.
            editor.CreateFreeText(rect, "This is a free text annotation.", 1);

            // Save the modified document.
            editor.Save(outputPath);

            Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}