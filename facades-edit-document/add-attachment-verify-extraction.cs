using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the file to attach, and the resulting PDF
        const string originalPdfPath   = "original.pdf";
        const string attachmentPath    = "sample_attachment.txt";
        const string pdfWithAttachment = "with_attachment.pdf";

        // Verify that source files exist
        if (!File.Exists(originalPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {originalPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found – {attachmentPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Add the attachment to the PDF using PdfContentEditor
        // ------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(originalPdfPath);
        // Add the attachment without any visual annotation
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");
        // Save the modified PDF
        editor.Save(pdfWithAttachment);
        editor.Close(); // Close the facade (optional, but good practice)

        // ------------------------------------------------------------
        // Step 2: Extract the attachment from the modified PDF
        // ------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfWithAttachment);
        // Must call ExtractAttachment before retrieving names or streams
        extractor.ExtractAttachment();

        // Get the list of attachment names
        var attachmentNames = extractor.GetAttachNames();
        if (attachmentNames == null || attachmentNames.Count == 0)
        {
            Console.Error.WriteLine("No attachments were found in the PDF.");
            return;
        }

        // Retrieve all attachment streams
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        // ------------------------------------------------------------
        // Step 3: Compare each extracted attachment with the original file
        // ------------------------------------------------------------
        byte[] originalBytes = File.ReadAllBytes(attachmentPath);
        bool allMatch = true;

        for (int i = 0; i < attachmentStreams.Length; i++)
        {
            string name = attachmentNames[i];
            MemoryStream ms = attachmentStreams[i];
            ms.Position = 0; // Ensure we read from the beginning

            // Read the extracted attachment into a byte array
            byte[] extractedBytes;
            using (MemoryStream temp = new MemoryStream())
            {
                ms.CopyTo(temp);
                extractedBytes = temp.ToArray();
            }

            // Simple byte‑wise comparison
            bool isEqual = originalBytes.Length == extractedBytes.Length;
            if (isEqual)
            {
                for (int j = 0; j < originalBytes.Length; j++)
                {
                    if (originalBytes[j] != extractedBytes[j])
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            Console.WriteLine($"Attachment \"{name}\": {(isEqual ? "MATCHES" : "DIFFERS")}");
            if (!isEqual) allMatch = false;
        }

        Console.WriteLine(allMatch
            ? "All extracted attachments match the original file."
            : "One or more attachments differ from the original file.");

        // Clean up facades
        extractor.Close();
    }
}