using System;
using System.IO;
using Aspose.Pdf; // Core API for PDF manipulation

class Program
{
    static void Main(string[] args)
    {
        // Allow the input and output directories to be supplied via command‑line arguments.
        // If not provided, fall back to sensible defaults.
        const string defaultInputDirectory = @"C:\InputPdfs";
        const string defaultOutputDirectory = @"C:\FlattenedPdfs";

        string inputDirectory = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? args[0]
            : defaultInputDirectory;
        string outputDirectory = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
            ? args[1]
            : defaultOutputDirectory;

        // Ensure the input directory exists before proceeding.
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Ensure the output directory exists (creates it if necessary).
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory.
        foreach (string inputFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Determine the output file path (same file name, different folder).
            string outputFilePath = Path.Combine(outputDirectory, Path.GetFileName(inputFilePath));

            // Load, flatten, and save the PDF using Aspose.Pdf core API.
            using (Document pdfDocument = new Document(inputFilePath))
            {
                // Remove form fields and annotations, placing their values directly on the page.
                pdfDocument.Flatten();

                // Save the flattened PDF to the custom output directory.
                pdfDocument.Save(outputFilePath);
            }

            Console.WriteLine($"Flattened PDF saved to: {outputFilePath}");
        }
    }
}
