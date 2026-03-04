using System;
using System.Drawing;               // Required for Rectangle and Color used by PdfContentEditor
using Aspose.Pdf.Facades;           // Facade classes for PDF editing

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the result PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output_js.pdf";

        // Ensure the source file exists before proceeding
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so use a using block for deterministic cleanup
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file to the editor
            editor.BindPdf(inputPath);

            // Add a JavaScript action that runs when the document is opened
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentOpen,
                "app.alert('Document opened');");

            // Add a JavaScript action that runs when the document is closed
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentClose,
                "app.alert('Good-bye!');");

            // Define a rectangle (in points) on page 1 where the JavaScript link will be placed
            // Rectangle(x, y, width, height) – origin is bottom‑left of the page
            Rectangle linkRect = new Rectangle(50, 700, 200, 750);

            // Create a clickable area that executes JavaScript when activated
            editor.CreateJavaScriptLink(
                "app.alert('Hello from link!');",
                linkRect,
                1,                     // Page number (1‑based indexing)
                Color.Blue);          // Color of the rectangle border

            // Save the modified PDF to the specified output path
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript actions saved to '{outputPath}'.");
    }
}