using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "source.pdf";          // PDF to which the attachment will be added
        const string outputPdfPath = "source_with_attachment.pdf";
        const string largeFilePath = "large_file.bin";      // Path to the large file to attach
        const string attachmentName = "large_file.bin";     // Name that will appear in the PDF attachment list

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(largeFilePath))
        {
            Console.Error.WriteLine($"Attachment source not found: {largeFilePath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the large file as a read‑only stream. The stream is passed directly to the
                // FileSpecification so the whole file is never loaded into memory.
                using (FileStream fileStream = new FileStream(
                    largeFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var fileSpec = new FileSpecification(attachmentName);
                    fileSpec.Contents = fileStream; // attach via stream
                    pdfDoc.EmbeddedFiles.Add(fileSpec);

                    // Save the modified PDF.
                    pdfDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
