using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";

        // List of files to attach
        string[] filesToAttach = { "attachment1.docx", "attachment2.jpg", "missingfile.txt" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                foreach (string filePath in filesToAttach)
                {
                    if (!File.Exists(filePath))
                    {
                        // Handle missing file: log and skip this attachment
                        Console.Error.WriteLine($"Warning: Attachment file not found and will be skipped: {filePath}");
                        continue;
                    }

                    // Create a FileSpecification for the attachment
                    var fileSpec = new FileSpecification(Path.GetFileName(filePath));
                    fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));

                    // Add the file specification to the PDF's embedded files collection
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Save the modified PDF
                doc.Save(outputPdf);
                Console.WriteLine($"PDF saved with attachments to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
