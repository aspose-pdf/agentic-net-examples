using System;
using System.IO;
using Aspose.Pdf;

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";
        // Path to the file that will be attached to every PDF
        const string attachmentPath = @"C:\Attachments\sample_attachment.pdf";
        const string attachmentDescription = "Sample attachment added to all documents";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Validate attachment exists before processing
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePdfPath in pdfFiles)
        {
            // Determine output file path (same name, different folder)
            string outputPdfPath = Path.Combine(outputFolder, Path.GetFileName(sourcePdfPath));

            try
            {
                // Load the source PDF using the pure‑managed Document class (no native DLL required)
                using (Document pdfDocument = new Document(sourcePdfPath))
                {
                    // Create a file specification for the attachment
                    var attachment = new FileSpecification(attachmentPath, attachmentDescription);
                    // Populate the file contents via a stream (required for EmbeddedFiles collection)
                    attachment.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

                    // Add the attachment to the PDF's EmbeddedFiles collection (Attachments collection is obsolete)
                    pdfDocument.EmbeddedFiles.Add(attachment);

                    // Save the modified PDF
                    pdfDocument.Save(outputPdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(sourcePdfPath)} → {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch attachment processing completed.");
    }
}
