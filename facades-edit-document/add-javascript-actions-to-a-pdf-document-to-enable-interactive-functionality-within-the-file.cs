using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Rectangle and Color used by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the PDF content editor facade
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the existing PDF file
            editor.BindPdf(inputPdf);

            // Add JavaScript actions that run when the document is opened and closed
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen,
                "app.alert('Document opened!');");
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentClose,
                "app.alert('Document closed!');");

            // Add a clickable area on page 1 that runs a JavaScript alert
            string jsCode = "app.alert('You clicked the JavaScript link!');";
            Rectangle linkRect = new Rectangle(100, 700, 300, 750); // left, top, width, height
            editor.CreateJavaScriptLink(jsCode, linkRect, 1, Color.Blue);

            // Save the modified PDF
            editor.Save(outputPdf);

            Console.WriteLine($"JavaScript actions added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}