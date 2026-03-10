using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfAttachmentManager
{
    // Adds a file attachment to a PDF document and saves the result.
    static void AddAttachment(string pdfPath, string attachmentPath, string description, string outputPath)
    {
        // Ensure source files exist.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // PdfContentEditor handles attachment addition without needing a Document instance.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(pdfPath);
            // Add the external file as a document attachment (no visual annotation).
            editor.AddDocumentAttachment(attachmentPath, description);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }

    // Extracts all embedded file attachments from a PDF document into the specified folder.
    static void ExtractAllAttachments(string pdfPath, string outputDirectory)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            // Populate internal attachment collection.
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names.
            var attachNames = extractor.GetAttachNames(); // Returns IList
            foreach (var nameObj in attachNames)
            {
                string name = nameObj as string;
                if (string.IsNullOrEmpty(name))
                    continue;

                // Extract the attachment to a temporary file (current working directory).
                extractor.ExtractAttachment(name);
                // Move the extracted file to the desired output directory.
                string sourcePath = Path.Combine(Directory.GetCurrentDirectory(), name);
                string destPath = Path.Combine(outputDirectory, name);
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destPath, overwrite: true);
                    Console.WriteLine($"Extracted: {destPath}");
                }
            }
        }
    }

    // Removes all embedded file attachments from a PDF document and saves the cleaned PDF.
    static void RemoveAllAttachments(string pdfPath, string outputPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(pdfPath);
            // Delete every attachment present in the document.
            editor.DeleteAttachments();
            editor.Save(outputPath);
        }

        Console.WriteLine($"All attachments removed. Saved to '{outputPath}'.");
    }

    static void Main()
    {
        // Example file paths (adjust as needed).
        string inputPdf = "input.pdf";
        string attachmentFile = "sample.docx";
        string description = "Sample document attachment";

        // 1. Add an attachment.
        string pdfWithAttachment = "with_attachment.pdf";
        AddAttachment(inputPdf, attachmentFile, description, pdfWithAttachment);

        // 2. Extract all attachments from the newly created PDF.
        string extractionFolder = "ExtractedAttachments";
        ExtractAllAttachments(pdfWithAttachment, extractionFolder);

        // 3. Remove all attachments from the PDF.
        string cleanedPdf = "cleaned.pdf";
        RemoveAllAttachments(pdfWithAttachment, cleanedPdf);
    }
}