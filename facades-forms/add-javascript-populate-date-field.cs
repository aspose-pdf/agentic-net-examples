using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF with a form field named "Date"
        const string outputPdf = "output_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor facade to add a JavaScript action that runs when the document is opened.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPdf);

            // JavaScript code: set the value of the field named "Date" to the current date.
            string jsCode = "this.getField('Date').value = new Date().toLocaleDateString();";

            // Add the JavaScript as a document open action.
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPdf}'.");
    }
}