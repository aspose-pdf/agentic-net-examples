using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";               // source PDF
        const string attachmentToAdd   = "sample_attachment.docx"; // file to embed
        const string description       = "Sample attachment file";
        const string outputPdfPath     = "output_with_attachment.pdf"; // PDF after adding attachment
        const string extractedFolder   = "ExtractedAttachments";   // folder for extracted files

        // Ensure the extraction folder exists
        Directory.CreateDirectory(extractedFolder);

        // ------------------------------------------------------------
        // 1. Add a document attachment (no annotation) using PdfContentEditor
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            // Add the attachment
            editor.AddDocumentAttachment(attachmentToAdd, description);

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Extract all attachments from the modified PDF using PdfExtractor
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF that now contains the attachment
            extractor.BindPdf(outputPdfPath);

            // Prepare extraction of attachments
            extractor.ExtractAttachment();

            // Write each attachment to the specified folder
            // GetAttachment(string) saves all attachments into the folder
            extractor.GetAttachment(extractedFolder);
        }

        Console.WriteLine("Attachment added and extracted successfully.");
    }
}