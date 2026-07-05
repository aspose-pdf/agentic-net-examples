using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Folder containing source PDF files
        const string sourceFolder = @"C:\PdfFolder\Input";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfFolder\Output";
        // Path to the attachment file to be added to each PDF
        const string attachmentPath = @"C:\Attachments\sample.txt";

        // Validate folders and attachment
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the source folder
        foreach (string pdfFile in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFile))
                {
                    // Create a file specification for the attachment using the constructor
                    // The second argument is a description (optional, can be empty string)
                    FileSpecification attachment = new FileSpecification(attachmentPath, "Embedded attachment");

                    // Add the attachment to the document's embedded files collection
                    doc.EmbeddedFiles.Add(attachment);

                    // Build the output file path (same name, different folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

                    // Save the modified PDF (PDF format, no SaveOptions needed)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch attachment processing completed.");
    }
}
