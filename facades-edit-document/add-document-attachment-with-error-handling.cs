using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output_with_attachment.pdf";
        const string attachmentFilePath = "attachment_file.pdf";
        const string attachmentDesc    = "Sample attachment description";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        // Verify that the attachment file exists before calling AddDocumentAttachment
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found – '{attachmentFilePath}'.");
            return;
        }

        try
        {
            // Use the PdfContentEditor facade to bind, modify, and save the PDF
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdfPath);                                   // load source PDF
                editor.AddDocumentAttachment(attachmentFilePath, attachmentDesc); // add attachment
                editor.Save(outputPdfPath);                                      // persist changes
            }

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}