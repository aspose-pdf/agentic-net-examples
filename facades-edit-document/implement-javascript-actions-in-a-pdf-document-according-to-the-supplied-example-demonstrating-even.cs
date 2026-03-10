using System;
using System.Drawing;                     // Required for System.Drawing.Rectangle and System.Drawing.Color used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;             // For JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPdf  = "sample.pdf";
        const string outputPdf = "sample_with_js.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (a SaveableFacade) inside a using block for deterministic disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // ------------------------------------------------------------
            // 1. Add JavaScript actions for document events (Open & Close)
            // ------------------------------------------------------------
            // DocumentOpen event – show a welcome alert when the PDF is opened
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentOpen,
                "app.alert('Welcome to the PDF!');");

            // DocumentClose event – show a goodbye alert when the PDF is closed
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentClose,
                "app.alert('Good‑bye!');");

            // ------------------------------------------------------------
            // 2. Create a clickable area (link) that runs JavaScript
            // ------------------------------------------------------------
            // Define a rectangle (in points) on page 1 where the link will be active
            // (x, y, width, height) – here a 150x50 rectangle at (100,600)
            System.Drawing.Rectangle jsLinkRect = new System.Drawing.Rectangle(100, 600, 150, 50);

            // JavaScript code to be executed when the rectangle is clicked
            string jsCode = "app.alert('You clicked the JavaScript link!');";

            // Create the JavaScript link on page 1 with a visible red border
            editor.CreateJavaScriptLink(
                jsCode,
                jsLinkRect,
                originalPage: 1,
                color: System.Drawing.Color.Red);

            // ------------------------------------------------------------
            // 3. Save the modified PDF
            // ------------------------------------------------------------
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript actions saved to '{outputPdf}'.");
    }
}
