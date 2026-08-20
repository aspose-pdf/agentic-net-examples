using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath  = Path.Combine(outputFolder, $"{fileName}_bates.pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Add Bates numbering to all pages.
                    // StartNumber = 5 (first Bates number)
                    // NumberOfDigits = 6 (e.g., 000005, 000006, …)
                    // Increment is the default page‑to‑page increment (1). 
                    // If a different step is required, custom logic would be needed,
                    // but the core API only supports sequential numbering.
                    doc.Pages.AddBatesNumbering(artifact =>
                    {
                        artifact.StartNumber    = 5;   // first number
                        artifact.NumberOfDigits = 6;   // zero‑padded to 6 digits
                        artifact.Prefix         = "Bates-";
                        // Optional: set position or alignment if needed
                        // artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
                        // artifact.BottomMargin = 20;
                    });

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}