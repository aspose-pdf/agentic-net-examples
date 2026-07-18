using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor facade to add a JavaScript action that runs on document open.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF.
            editor.BindPdf(inputPdf);

            // JavaScript code: set the value of the field named "Date" to the current date.
            string jsCode = "this.getField('Date').value = new Date().toLocaleDateString();";

            // Add the script to the Document Open event.
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPdf}'.");
    }
}