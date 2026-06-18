using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "source.pdf";               // Original PDF
        const string attachmentPath = "attachment.bin";          // File to attach
        const string outputPdfPath = "withAttachment.pdf";       // PDF after adding attachment
        const string extractedPath = "extracted.bin";            // Where the extracted file will be saved

        // Verify that source files exist
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

        // ------------------------------------------------------------
        // 1. Add the attachment to the PDF using PdfContentEditor
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(sourcePdfPath);
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
            // Must call ExtractAttachment before retrieving names or streams
            extractor.ExtractAttachment();

            IList<string> attachmentNames = extractor.GetAttachNames();
            if (attachmentNames == null || attachmentNames.Count == 0)
            {
                Console.Error.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Assuming only one attachment was added
            string extractedFileName = attachmentNames[0];
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Find the stream that corresponds to the extracted file name
            // The order of streams matches the order of names returned by GetAttachNames()
            int index = attachmentNames.IndexOf(extractedFileName);
            if (index < 0 || index >= attachmentStreams.Length)
            {
                Console.Error.WriteLine("Mismatch between attachment names and streams.");
                return;
            }

            MemoryStream extractedStream = attachmentStreams[index];
            extractedStream.Position = 0; // Ensure we read from the beginning

            // Save the extracted attachment to disk (optional, for visual verification)
            using (FileStream fs = new FileStream(extractedPath, FileMode.Create, FileAccess.Write))
            {
                extractedStream.CopyTo(fs);
            }

            // ------------------------------------------------------------
            // 3. Compare the original attachment with the extracted one
            // ------------------------------------------------------------
            byte[] originalBytes = File.ReadAllBytes(attachmentPath);
            byte[] extractedBytes = File.ReadAllBytes(extractedPath);

            bool areEqual = originalBytes.Length == extractedBytes.Length;
            if (areEqual)
            {
                for (int i = 0; i < originalBytes.Length; i++)
                {
                    if (originalBytes[i] != extractedBytes[i])
                    {
                        areEqual = false;
                        break;
                    }
                }
            }

            Console.WriteLine(areEqual
                ? "Success: Extracted attachment matches the original file."
                : "Failure: Extracted attachment does NOT match the original file.");
        }
    }
}