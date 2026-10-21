using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Path to the attachment that will be added to each PDF
        const string attachmentPath = "attachment.txt";

        // Directory where the output PDFs will be saved
        const string outputDir = "Output";

        // Verify the attachment exists before processing
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        foreach (string pdfPath in pdfFiles)
        {
            // Skip missing source PDFs
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            // Build the output file name (original name + suffix)
            string outputPath = Path.Combine(
                outputDir,
                Path.GetFileNameWithoutExtension(pdfPath) + "_attached.pdf");

            try
            {
                // Load the source PDF using Document (Aspose.Pdf)
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Create a FileSpecification for the attachment
                    var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
                    {
                        // Load the attachment bytes into a stream
                        Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
                    };

                    // Add the file specification to the EmbeddedFiles collection
                    pdfDoc.EmbeddedFiles.Add(fileSpec);

                    // Save the modified PDF to the output location
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Attachment added: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
