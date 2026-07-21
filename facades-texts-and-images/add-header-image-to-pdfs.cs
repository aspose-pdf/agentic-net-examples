using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the header image that will be added to each PDF.
        const string headerImagePath = "header.jpg";

        // List of PDF files to process.
        string[] inputPdfFiles = new[]
        {
            "document1.pdf",
            "document2.pdf",
            "document3.pdf"
        };

        // Directory where the output PDFs with headers will be saved.
        const string outputDirectory = "ProcessedPdfs";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Verify that the header image exists before processing.
        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        foreach (string inputPath in inputPdfFiles)
        {
            // Skip missing input files.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                continue;
            }

            // Build the output file path.
            string outputPath = Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputPath) + "_withHeader.pdf");

            // Use PdfFileStamp to add the header image.
            using (PdfFileStamp stamp = new PdfFileStamp())
            {
                // Bind the source PDF.
                stamp.BindPdf(inputPath);

                // Add the header image. Top margin is set to 20 points.
                stamp.AddHeader(headerImagePath, 20f);

                // Save the modified PDF to the output path.
                stamp.Save(outputPath);

                // Close the stamp facade (writes any remaining changes).
                stamp.Close();
            }

            Console.WriteLine($"Processed: {inputPath} → {outputPath}");
        }
    }
}