using System;
using System.Drawing;               // Required for Rectangle and Color used by PdfContentEditor
using Aspose.Pdf.Facades;          // Facade classes for editing PDF content
using Aspose.Pdf.Annotations;       // PredefinedAction enum

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use a using block to guarantee disposal of the facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // -----------------------------------------------------------------
            // 1. Add JavaScript actions that fire on document events (open/close)
            // -----------------------------------------------------------------
            // DocumentOpen event – show a welcome alert when the PDF is opened
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentOpen,
                "app.alert('Welcome to the PDF!');");

            // DocumentClose event – show a goodbye alert when the PDF is closed
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentClose,
                "app.alert('Good-bye!');");

            // ---------------------------------------------------------------
            // 2. Create a clickable rectangle that runs JavaScript when clicked
            // ---------------------------------------------------------------
            // Define the clickable area (x, y, width, height) on page 1
            Rectangle jsRect = new Rectangle(0, 0, 150, 50);
            // JavaScript code to be executed on click
            string jsCode = "app.alert('You clicked the JavaScript link!');";
            // Create the link on page 1 with a visible red border
            editor.CreateJavaScriptLink(jsCode, jsRect, 1, Color.Red);

            // ---------------------------------------------------------------
            // 3. Create a custom action link that triggers predefined actions
            // ---------------------------------------------------------------
            // Define another clickable area on page 1
            Rectangle customRect = new Rectangle(0, 60, 150, 50);
            // Build an array of predefined actions (e.g., go to first page and open print dialog)
            Enum[] actions = new Enum[]
            {
                PredefinedAction.FirstPage,
                PredefinedAction.PrintDialog
            };
            // Create the custom action link with a blue border
            editor.CreateCustomActionLink(customRect, 1, Color.Blue, actions);

            // ---------------------------------------------------------------
            // 4. Save the modified PDF
            // ---------------------------------------------------------------
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript actions saved to '{outputPdf}'.");
    }
}