using System;
using System.IO;
using System.Drawing;               // System.Drawing.Rectangle for annotation bounds
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfContentEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF and edit it via the facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) in points
            // NOTE: PdfContentEditor.CreateFreeText expects System.Drawing.Rectangle, not Aspose.Pdf.Rectangle
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // HTML content to be displayed inside the free‑text annotation
            string htmlContent = "<b>Bold Text</b> <i>Italic Text</i> <u>Underlined</u>";

            // Create a free‑text annotation on page 1 with the HTML string
            editor.CreateFreeText(rect, htmlContent, 1);

            // Persist the changes
            editor.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with HTML added and saved to '{outputPath}'.");
    }
}
