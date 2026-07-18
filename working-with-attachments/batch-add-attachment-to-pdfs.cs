using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Facades;      // For FileSpecification if needed (also in Aspose.Pdf)

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";
        // Path to the file that will be attached to every PDF
        const string attachmentPath = @"C:\Attachment\sample.txt";

        // Validate folders and attachment
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

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block (ensures disposal)
                using (Document doc = new Document(pdfFile))
                {
                    // Create a FileSpecification for the attachment
                    // The constructor accepts the file path of the attachment
                    FileSpecification attachment = new FileSpecification(attachmentPath);

                    // Add the attachment to the document's EmbeddedFiles collection
                    doc.EmbeddedFiles.Add(attachment);

                    // Build the output file path (preserve original file name)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

                    // Save the modified PDF (no SaveOptions needed for PDF output)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch attachment operation completed.");
    }
}