using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class AddJavaScriptToPdf
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Use PdfContentEditor (which implements IDisposable) inside a using block
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // -----------------------------------------------------------------
            // 1. Add a document‑level JavaScript action that runs when the PDF is opened
            // -----------------------------------------------------------------
            // PdfContentEditor.DocumentOpen is a constant string representing the event type.
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentOpen,
                "app.alert('Welcome to the PDF!');");

            // -----------------------------------------------------------------
            // 2. Add a clickable rectangle on page 1 that executes JavaScript when clicked
            // -----------------------------------------------------------------
            // Define the clickable area (left, top, width, height) in points.
            Rectangle jsRect = new Rectangle(100, 600, 200, 50); // x=100, y=600, w=200, h=50
            // The JavaScript code to be executed on click.
            string jsCode = "app.alert('You clicked the JavaScript link!');";
            // Create the link on page 1 with a visible red border.
            editor.CreateJavaScriptLink(jsCode, jsRect, 1, Color.Red);

            // Save the modified PDF to the output file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript actions added successfully. Output saved to '{outputPdf}'.");
    }
}