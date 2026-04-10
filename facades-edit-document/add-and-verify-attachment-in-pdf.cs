using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";          // PDF to which we will add the attachment
        const string attachmentPath    = "attachment_file.pdf"; // File to attach
        const string outputPdfPath     = "output_with_attachment.pdf"; // Resulting PDF
        const string extractFolderPath = "extracted_attachments";

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
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            // Add the attachment (no visual annotation)
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");

            // Save the modified PDF (PDF format – no SaveOptions needed)
            editor.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Extract the attachment from the modified PDF
        // ------------------------------------------------------------
        // Ensure the extraction folder exists
        Directory.CreateDirectory(extractFolderPath);

        // Read original attachment bytes once for comparison
        byte[] originalBytes = File.ReadAllBytes(attachmentPath);
        bool matchFound = false;

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF that contains the attachment
            extractor.BindPdf(outputPdfPath);

            // Extract all attachments into memory streams
            extractor.ExtractAttachment();

            // Get attachment names (parallel to streams)
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve the streams
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Iterate over extracted attachments
            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string name = attachmentNames[i];
                MemoryStream ms = attachmentStreams[i];

                // Reset stream position before reading
                ms.Position = 0;
                byte[] extractedBytes = ms.ToArray();

                // Save extracted file (optional, for visual verification)
                string extractedFilePath = Path.Combine(extractFolderPath, name);
                File.WriteAllBytes(extractedFilePath, extractedBytes);

                // Compare content with the original file
                bool isEqual = originalBytes.SequenceEqual(extractedBytes);
                Console.WriteLine($"Attachment '{name}': content match = {isEqual}");

                if (isEqual)
                    matchFound = true;
            }
        }

        if (matchFound)
            Console.WriteLine("Verification succeeded – extracted attachment matches the original file.");
        else
            Console.WriteLine("Verification failed – no matching attachment found.");
    }
}