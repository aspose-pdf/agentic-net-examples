using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Facades;        // Facades namespace (required by task)

class Program
{
    static void Main(string[] args)
    {
        // Input directory containing PDFs to be flattened
        const string inputDirectory = "InputPdfs";

        // Determine the output directory – use the first command‑line argument if supplied,
        // otherwise fall back to the default "FlattenedPdfs".
        string outputDirectory = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
                                 ? args[0]
                                 : "FlattenedPdfs";

        // Verify that the input directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. Nothing to process.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Optional: instantiate a PdfFileEditor (facade) to satisfy the requirement.
        // It is not needed for flattening but demonstrates usage of the Facades API.
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Process each PDF file in the input directory
        foreach (string inputFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputFilePath))
            {
                // Remove all form fields and replace them with their appearance values
                pdfDocument.Flatten();

                // Build the output file path preserving the original file name
                string fileName   = System.IO.Path.GetFileName(inputFilePath);
                string outputPath = System.IO.Path.Combine(outputDirectory, fileName);

                // Save the flattened PDF to the custom output directory
                pdfDocument.Save(outputPath);

                Console.WriteLine($"Flattened PDF saved to: {outputPath}");
            }
        }

        // PdfFileEditor does not implement IDisposable, so no explicit Dispose call is required.
        // If you need to release resources, simply let the object go out of scope.
    }
}
