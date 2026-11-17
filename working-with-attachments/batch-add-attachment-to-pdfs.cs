using System;
using System.IO;
using Aspose.Pdf;

class BatchAttach
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";
        // Folder where processed PDFs will be saved (can be the same as inputFolder)
        const string outputFolder = @"C:\PdfFolder\Processed";
        // Path to the attachment file that will be added to each PDF
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

        // Read the attachment once – reuse the byte array for every PDF
        byte[] attachmentBytes = File.ReadAllBytes(attachmentPath);
        string attachmentFileName = Path.GetFileName(attachmentPath);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                using (Document doc = new Document(pdfFile))
                {
                    // Create a FileSpecification for the attachment
                    var fileSpec = new FileSpecification(attachmentFileName)
                    {
                        // The file data is supplied via a MemoryStream
                        Contents = new MemoryStream(attachmentBytes)
                    };

                    // Add the file specification to the EmbeddedFiles collection
                    doc.EmbeddedFiles.Add(fileSpec);

                    // Build output file path (preserve original file name)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch attachment completed.");
    }
}
