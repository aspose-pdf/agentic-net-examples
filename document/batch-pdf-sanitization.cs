using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization; // for SanitizationException

class BatchPdfSanitizer
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchPdfSanitizer <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle: load)
                using (Document doc = new Document(inputPath))
                {
                    // Perform sanitization steps
                    // Remove metadata, flatten form fields, and optimize resources
                    doc.RemoveMetadata();
                    doc.Flatten();
                    doc.OptimizeResources();

                    // Save the cleaned PDF (lifecycle: save)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {fileName} -> {outputPath}");
            }
            catch (SanitizationException sanitEx)
            {
                // Handle sanitization-specific errors
                Console.Error.WriteLine($"Sanitization failed for {fileName}: {sanitEx.Message}");
            }
            catch (Exception ex)
            {
                // General error handling
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch sanitization completed.");
    }
}