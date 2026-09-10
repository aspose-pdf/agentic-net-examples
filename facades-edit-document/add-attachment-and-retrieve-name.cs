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
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentFilePath = "attachment.txt";

        // ---------------------------------------------------------------
        // 1. Ensure the required files exist (self‑contained example)
        // ---------------------------------------------------------------
        // Create a minimal PDF if it does not exist
        if (!File.Exists(inputPdfPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPdfPath);
            }
        }

        // Create a simple attachment file if it does not exist
        if (!File.Exists(attachmentFilePath))
        {
            File.WriteAllText(attachmentFilePath, "Sample attachment content");
        }

        // -----------------------------------------------------------------
        // 2. Add a document attachment using PdfContentEditor (Facades API)
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            // Add the file as an attachment with a description
            editor.AddDocumentAttachment(attachmentFilePath, "Sample attachment description");
            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // 3. Retrieve attachment information using PdfExtractor (Facades API)
        // ---------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdfPath);
            // Extract attachment metadata (required before calling GetAttachNames or GetAttachmentInfo)
            extractor.ExtractAttachment();

            // Retrieve and display attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();
            Console.WriteLine("Attachment names extracted from the PDF:");
            foreach (string name in attachmentNames)
            {
                Console.WriteLine($"- {name}");
            }

            // Retrieve full FileSpecification objects (if further details are needed)
            List<FileSpecification> specs = extractor.GetAttachmentInfo();
            Console.WriteLine("\nFileSpecification details:");
            foreach (FileSpecification spec in specs)
            {
                // The original file name of the embedded file is available via the Name property
                Console.WriteLine($"- {spec.Name}");
            }
        }
    }
}
