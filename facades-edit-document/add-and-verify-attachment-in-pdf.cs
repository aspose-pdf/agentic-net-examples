using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";          // Original PDF
        const string attachmentPath    = "attachment.bin";    // File to attach
        const string outputPdfPath     = "output_with_attachment.pdf";

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

        // ------------------------------------------------------------
        // 1. Add the attachment to the PDF using PdfContentEditor
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            // AddDocumentAttachment adds the file without any visual annotation
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
            editor.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Extract the attachment from the edited PDF using PdfExtractor
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdfPath);
            // Must call ExtractAttachment before accessing attachment info
            extractor.ExtractAttachment();

            // Retrieve attachment names (should contain our file name)
            IList<string> attachNames = extractor.GetAttachNames();
            if (attachNames.Count == 0)
            {
                Console.Error.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Retrieve all attachment streams
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Assuming only one attachment was added, locate it
            int index = attachNames.IndexOf(Path.GetFileName(attachmentPath));
            if (index < 0 || index >= attachmentStreams.Length)
            {
                Console.Error.WriteLine("Added attachment not found among extracted items.");
                return;
            }

            MemoryStream extractedStream = attachmentStreams[index];
            extractedStream.Position = 0; // Ensure stream is at the beginning

            // ------------------------------------------------------------
            // 3. Compare the extracted content with the original file
            // ------------------------------------------------------------
            byte[] originalBytes = File.ReadAllBytes(attachmentPath);
            byte[] extractedBytes = new byte[extractedStream.Length];
            extractedStream.Read(extractedBytes, 0, extractedBytes.Length);

            bool isIdentical = originalBytes.Length == extractedBytes.Length;
            if (isIdentical)
            {
                for (int i = 0; i < originalBytes.Length; i++)
                {
                    if (originalBytes[i] != extractedBytes[i])
                    {
                        isIdentical = false;
                        break;
                    }
                }
            }

            Console.WriteLine(isIdentical
                ? "Attachment extracted successfully and matches the original file."
                : "Attachment extraction failed or content does not match.");
        }
    }
}