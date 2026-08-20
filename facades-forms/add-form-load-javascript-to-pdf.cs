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

        // Use PdfContentEditor facade to add a document‑level JavaScript action.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF.
            editor.BindPdf(inputPdf);

            // JavaScript that runs when the document is opened and sets the "Date" field.
            string jsCode = "this.getField('Date').value = new Date().toLocaleDateString();";

            // Add the script to the DocumentOpen event.
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with form‑load JavaScript saved to '{outputPdf}'.");
    }
}