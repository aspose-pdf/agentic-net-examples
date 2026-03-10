using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Rectangle and System.Drawing.Color used by CreateJavaScriptLink

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_with_js.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the existing PDF document
        editor.BindPdf(inputPath);

        // --------------------------------------------------------------------
        // Add JavaScript actions that are triggered by document events
        // --------------------------------------------------------------------
        // Action executed when the document is opened
        editor.AddDocumentAdditionalAction(
            PdfContentEditor.DocumentOpen,
            "app.alert('Welcome to the PDF!');");

        // Action executed just before the document is printed
        editor.AddDocumentAdditionalAction(
            PdfContentEditor.DocumentWillPrint,
            "app.alert('Document is about to be printed.');");

        // --------------------------------------------------------------------
        // Add a clickable area (link) on page 1 that runs JavaScript when clicked
        // --------------------------------------------------------------------
        // Define the rectangle area (x, y, width, height) using System.Drawing.Rectangle
        System.Drawing.Rectangle jsRect = new System.Drawing.Rectangle(100, 500, 200, 50);
        // Create the JavaScript link on page 1 with a visible red border using System.Drawing.Color
        editor.CreateJavaScriptLink(
            "app.alert('You clicked the link!');",
            jsRect,
            1, // page number (1‑based)
            System.Drawing.Color.Red);

        // Save the modified PDF to a new file
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with JavaScript actions to '{outputPath}'.");
    }
}
