using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // existing PDF
        const string attachmentPath = "attachment_file.pdf"; // file to attach
        const string outputPdfPath  = "output.pdf";         // result PDF
        const string description    = "Description of attachment_file";

        // Verify that the source PDF and attachment exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Use PdfContentEditor (a SaveableFacade) to add the attachment
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Add the file attachment (no visual annotation)
            editor.AddDocumentAttachment(attachmentPath, description);

            // Save the modified PDF to a new file
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}