using System;
using System.IO;
using Aspose.Pdf;

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Output folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";
        // Path to the file that will be attached to every PDF
        const string attachmentPath = @"C:\Attachments\sample.txt";

        // Validate paths
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Determine output file name (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Create a FileSpecification for the attachment.
                    // The constructor takes the name that will appear in the PDF and a description.
                    var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), "Attachment");

                    // Load the attachment bytes into a MemoryStream and assign it to the Contents property.
                    fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

                    // Add the file specification to the EmbeddedFiles collection.
                    doc.EmbeddedFiles.Add(fileSpec);

                    // Save the modified PDF to the output location
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch attachment processing completed.");
    }
}
