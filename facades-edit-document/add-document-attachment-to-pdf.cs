using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // PDF to which the attachment will be added
        const string outputPdf  = "output.pdf";     // Resulting PDF with the attachment
        const string attachment = "Terms.pdf";      // File to attach
        const string description = "Contract Terms";

        // Verify that source PDF and attachment exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Use PdfContentEditor to bind the PDF, add the attachment, and save the result
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);                                   // Load the PDF
        editor.AddDocumentAttachment(attachment, description);      // Attach the file (no annotation)
        editor.Save(outputPdf);                                     // Persist changes

        Console.WriteLine($"Attachment '{attachment}' added to '{outputPdf}'.");
    }
}