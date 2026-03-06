using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF containing attachments
        const string outputPdf = "output.pdf";     // PDF after attachments are removed

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfContentEditor provides methods to edit a PDF, including removal of attachments
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Delete all embedded file attachments from the document
            editor.DeleteAttachments();

            // Save the modified document as a PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachments removed. Saved file: '{outputPdf}'.");
    }
}