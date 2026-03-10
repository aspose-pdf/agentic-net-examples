using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_with_js.pdf";
        const string strippedPath = "sample_stripped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF and add JavaScript actions using PdfContentEditor
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPath);

            // Add a document-level JavaScript action that runs when the document is opened
            // PdfContentEditor.DocumentOpen is a constant representing the "Open" event
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen,
                "app.alert('Document opened!');");

            // Add a JavaScript link annotation on page 1.
            // The rectangle defines the clickable area (x, y, width, height) in points.
            // The color parameter sets the border color of the link annotation.
            editor.CreateJavaScriptLink(
                "app.alert('Link clicked!');",
                new Rectangle(100, 500, 200, 100), // left=100, top=500, width=200, height=100
                1,                                 // page number (1‑based)
                Color.Blue);                       // border color

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");

        // -----------------------------------------------------------------
        // 2. Remove all JavaScript from the PDF using PdfJavaScriptStripper
        // -----------------------------------------------------------------
        PdfJavaScriptStripper stripper = new PdfJavaScriptStripper();
        stripper.Strip(outputPath, strippedPath);

        Console.WriteLine($"All JavaScript stripped. Result saved to '{strippedPath}'.");
    }
}