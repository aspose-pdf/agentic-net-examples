using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentFilePath = "attachment_file.pdf";
        const string attachmentDescription = "Sample attachment";

        // Verify that source PDF and attachment exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Create the PdfContentEditor facade, bind the PDF, add the attachment, and save
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(inputPdfPath);

            // Add the attachment without a visual annotation
            editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

            // Persist the changes to a new PDF file
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}