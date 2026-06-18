using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf      = "input.pdf";          // existing PDF
        const string attachmentFile = "attachment.txt";    // file to attach
        const string outputPdf     = "output_with_attachment.pdf";

        // Verify source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add a document attachment (no visible annotation)
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            // description is optional; can be any string
            editor.AddDocumentAttachment(attachmentFile, "Sample attachment description");
            editor.Save(outputPdf);
        }

        // ------------------------------------------------------------
        // 2. Retrieve attachment information to verify the name
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdf);
            // GetAttachmentInfo returns a list of FileSpecification objects
            List<FileSpecification> attachedFiles = extractor.GetAttachmentInfo();

            if (attachedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // For demonstration, output the name of each attached file
            foreach (FileSpecification spec in attachedFiles)
            {
                // The Name property holds the original file name of the attachment
                Console.WriteLine($"Attachment name: {spec.Name}");
            }

            // Optional verification: check that the expected file name is present
            string expectedName = Path.GetFileName(attachmentFile);
            bool found = attachedFiles.Exists(f => string.Equals(f.Name, expectedName, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(found
                ? $"Verification succeeded: attachment \"{expectedName}\" is present."
                : $"Verification failed: attachment \"{expectedName}\" not found.");
        }
    }
}