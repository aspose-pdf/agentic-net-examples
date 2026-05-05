using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the resulting PDF
        const string sourcePdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // ---------- Add attachment ----------
        // PdfContentEditor is a Facades class used for editing PDFs.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(sourcePdfPath);                                   // Load the PDF
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment"); // Add the file as an attachment
            editor.Save(outputPdfPath);                                      // Save the modified PDF
        }

        // ---------- Retrieve attachment name for verification ----------
        // PdfExtractor extracts attachment information from a PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdfPath);    // Load the PDF that now contains the attachment
            extractor.ExtractAttachment();       // Required before calling GetAttachNames()
            IList<string> attachmentNames = extractor.GetAttachNames(); // List of attachment file specification names

            if (attachmentNames == null || attachmentNames.Count == 0)
            {
                Console.WriteLine("No attachments were found in the PDF.");
                return;
            }

            Console.WriteLine("Attachments found in the PDF:");
            foreach (string name in attachmentNames)
            {
                // The name returned is the file specification name stored in the PDF.
                // It usually matches the original file name, but we output it for verification.
                Console.WriteLine(name);
            }
        }
    }
}
