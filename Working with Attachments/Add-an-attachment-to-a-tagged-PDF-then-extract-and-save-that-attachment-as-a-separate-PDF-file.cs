using System;
using System.IO;
using System.Collections.Generic; // Added for IList<string>
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input_tagged.pdf";          // source tagged PDF
        const string attachmentPath = "attachment.pdf";          // file to attach (must be a PDF)
        const string attachmentDescription = "Sample PDF attachment";
        const string pdfWithAttachment = "output_with_attachment.pdf"; // PDF after adding attachment
        const string extractedAttachmentPath = "extracted_attachment.pdf"; // where to save extracted file

        // Verify required files exist
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

        // -------------------------------------------------
        // 1. Add an attachment to the tagged PDF
        // -------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPdfPath);

            // Add the attachment (no visual annotation, just a document attachment)
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

            // Save the modified PDF
            editor.Save(pdfWithAttachment);
        }

        // -------------------------------------------------
        // 2. Extract the attachment and save it as a separate PDF
        // -------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF that now contains the attachment
            extractor.BindPdf(pdfWithAttachment);

            // Extract attachment data into internal buffers
            extractor.ExtractAttachment();

            // Retrieve attachment names (generic IList<string>) and streams
            IList<string> attachNames = extractor.GetAttachNames();          // e.g., ["attachment.pdf"]
            MemoryStream[] attachStreams = extractor.GetAttachment(); // parallel array of streams

            // Iterate over all extracted attachments
            for (int i = 0; i < attachStreams.Length; i++)
            {
                // Get the name of the attachment
                string name = attachNames[i];

                // Determine output file name – here we use a fixed name for simplicity
                string outPath = extractedAttachmentPath;

                // Write the stream contents to a file
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Ensure the stream position is at the beginning
                    attachStreams[i].Position = 0;
                    attachStreams[i].CopyTo(fs);
                }

                Console.WriteLine($"Extracted attachment '{name}' saved to '{outPath}'.");
            }
        }
    }
}
