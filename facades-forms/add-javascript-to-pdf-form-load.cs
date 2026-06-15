using System;
using System.IO;
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

        // JavaScript that runs when the document is opened.
        // It sets the value of the form field named "Date" to today's date.
        string jsCode = "this.getField('Date').value = (new Date()).toLocaleDateString();";

        // Use the PdfContentEditor facade to add the JavaScript as a document‑open action.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPdf}");
    }
}