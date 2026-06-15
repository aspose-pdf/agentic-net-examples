using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing input PDFs
        const string inputFolder  = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Starting number for the first document
        int startNumber = 1;
        // Increment to apply after each document is processed
        const int increment = 5;

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Add Bates numbering to all pages of the document.
                    // The Action configures the BatesNArtifact for each document.
                    doc.Pages.AddBatesNumbering(artifact =>
                    {
                        artifact.StartNumber   = startNumber; // Set the starting number for this document
                        artifact.NumberOfDigits = 6;          // Use 6 digits (adjust as needed)
                        // Optional: set position, alignment, etc., if desired
                    });

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed '{fileName}' with start number {startNumber}.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }

            // Update the start number for the next document
            startNumber += increment;
        }

        Console.WriteLine("Batch Bates numbering completed.");
    }
}