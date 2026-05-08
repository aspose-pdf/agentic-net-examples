using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Not required for this task but kept for completeness

class BatchBatesNumbering
{
    static void Main()
    {
        // Folder containing input PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_bates.pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Counter that will be increased by 5 for each page
                    int batesCounter = 1;

                    // Add Bates numbering to each page.
                    // The Action configures the BatesNArtifact for the current page.
                    doc.Pages.AddBatesNumbering(artifact =>
                    {
                        artifact.StartNumber = batesCounter; // Set the number for this page
                        artifact.NumberOfDigits = 6;         // Default 6 digits (adjust as needed)
                        artifact.Prefix = "B-";              // Optional prefix
                        // Increment the counter by 5 for the next page
                        batesCounter += 5;
                    });

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} -> {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}