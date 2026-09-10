using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";        // source PDF
        const string attachment = "attachment.pdf";   // file to attach
        const string description = "Sample attachment for metadata enrichment";
        const string outputPdf  = "output.pdf";       // result PDF

        // Ensure the source files exist before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Create the facade, bind the PDF, add the attachment, and save the result
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);                                 // load PDF
        editor.AddDocumentAttachment(attachment, description);   // add attachment with label
        editor.Save(outputPdf);                                   // persist changes

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}