using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";
        // Path to the file that will be attached to every PDF
        const string attachmentPath = @"C:\Attachment\sample-attachment.pdf";

        // Validate paths
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFilePath))
                {
                    // Create a FileSpecification for the attachment using the constructor that accepts the file path
                    // The second argument is an optional description; we use the file name as a simple description.
                    FileSpecification attachmentSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath));

                    // Add the attachment to the document's EmbeddedFiles collection
                    string attachmentKey = Path.GetFileName(attachmentPath);
                    doc.EmbeddedFiles.Add(attachmentKey, attachmentSpec);

                    // Determine output file path (same name, different folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFilePath));

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed and saved: {Path.GetFileName(pdfFilePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfFilePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch attachment processing completed.");
    }
}
