using System;
using System.IO;
using Aspose.Pdf.Facades;               // Facades API
using Aspose.Pdf;                       // Core API (required for Document reference)

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

        // Create a PdfContentEditor (Facades) and bind the existing PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // JavaScript code that logs the current page number (1‑based) to the console
        // 'this.pageNum' is zero‑based, so add 1.
        string jsCode = "app.console.println('Current page: ' + (this.pageNum + 1));";

        // Add the JavaScript as a document‑open action.
        // PdfContentEditor defines constants for event types; use DocumentOpen.
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"PDF saved with JavaScript action: {outputPdf}");
    }
}