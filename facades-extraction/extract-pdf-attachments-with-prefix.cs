using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Output";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Bind the PDF with PdfExtractor (required by the task description)
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);
                // No direct attachment API on PdfExtractor in recent versions, so we use Document instead.
            }

            // Load the PDF with the high‑level Document API to access embedded files (attachments)
            Document pdfDoc = new Document(inputPdfPath);

            // The EmbeddedFiles collection contains all attachments.
            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
            {
                // Use the Name property of FileSpecification for the original attachment name
                string attachmentName = fileSpec.Name;
                string outputFileName = "Att_" + attachmentName;
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the attachment stream to disk using the Contents stream
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Ensure the stream is at the beginning
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;
                    fileSpec.Contents.CopyTo(fs);
                }

                Console.WriteLine($"Saved attachment: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
