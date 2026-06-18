using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Usage:
    //   dotnet run <headerImagePath> <outputDirectory> <inputPdf1> [<inputPdf2> ...]
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <headerImagePath> <outputDirectory> <inputPdf1> [<inputPdf2> ...]");
            return;
        }

        string headerImagePath = args[0];
        string outputDirectory = args[1];

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        if (!Directory.Exists(outputDirectory))
        {
            try
            {
                Directory.CreateDirectory(outputDirectory);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Process each input PDF file
        for (int i = 2; i < args.Length; i++)
        {
            string inputPdfPath = args[i];

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                continue;
            }

            // Build output file name: original name + "_header.pdf"
            string outputPdfPath = Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputPdfPath) + "_header.pdf");

            try
            {
                // Initialize PdfFileStamp facade
                PdfFileStamp fileStamp = new PdfFileStamp();

                // Bind the source PDF
                fileStamp.BindPdf(inputPdfPath);

                // Add the header image to all pages.
                // Top margin of 20 points (adjust as needed)
                fileStamp.AddHeader(headerImagePath, 20f);

                // Save the modified PDF
                fileStamp.Save(outputPdfPath);

                // Close the facade (releases resources)
                fileStamp.Close();

                Console.WriteLine($"Processed '{inputPdfPath}' -> '{outputPdfPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPdfPath}': {ex.Message}");
            }
        }
    }
}