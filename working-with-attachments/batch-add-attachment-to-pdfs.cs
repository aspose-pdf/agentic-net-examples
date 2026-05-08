using System;
using System.IO;
using Aspose.Pdf;

class BatchAttachmentProcessor
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";
        // Path to the file that will be attached to each PDF
        const string attachmentPath = @"C:\Attachment\attachment.pdf";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFile);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFile))
                {
                    // Create a file specification for the attachment using the constructor
                    FileSpecification attachmentSpec = new FileSpecification(attachmentPath, "Embedded attachment");

                    // Add the attachment to the document's EmbeddedFiles collection
                    doc.EmbeddedFiles.Add(attachmentSpec);

                    // Save the modified PDF to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch attachment processing completed.");
    }
}
