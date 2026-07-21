using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For any facade utilities (not used here)

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

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
                // Load the PDF document (lifecycle: load)
                using (Document doc = new Document(inputPath))
                {
                    // Add a Bates numbering artifact to each page.
                    // The number on page N will be: 1 + (N‑1) * 5
                    foreach (Page page in doc.Pages)
                    {
                        int batesNumber = 1 + (page.Number - 1) * 5;

                        // Create a new Bates numbering artifact.
                        // The constructor is internal, but the object can be created via the public API.
                        BatesNArtifact artifact = new BatesNArtifact();

                        // Set the text to the calculated number, padded to 6 digits (default length).
                        artifact.Text = batesNumber.ToString("D6");

                        // Optionally set alignment or margins here, e.g.:
                        // artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
                        // artifact.TopMargin = 20;

                        // Attach the artifact to the current page.
                        page.Artifacts.Add(artifact);
                    }

                    // Save the modified PDF (lifecycle: save)
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