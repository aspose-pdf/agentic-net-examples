using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;          // For ITaggedContent (if needed)
using Aspose.Pdf.LogicalStructure; // For structure element types (optional)

class BatchAutoTagger
{
    static void Main()
    {
        // Input folder containing untagged PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where tagged PDFs will be saved
        const string outputFolder = @"C:\TaggedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enable auto‑tagging globally (static settings)
        // This turns on the automatic tagging engine for all conversions
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Derive output file name (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Load the source PDF (untagged)
                using (Document doc = new Document(inputPath))
                {
                    // Prepare conversion options:
                    // - Convert to PDF/A‑1B (a tagged PDF format)
                    // - Apply the global AutoTaggingSettings
                    PdfFormatConversionOptions convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        AutoTaggingSettings = AutoTaggingSettings.Default
                    };

                    // Perform the conversion; this adds the tagging structure
                    doc.Convert(convertOptions);

                    // Save the newly tagged PDF to the output location
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Tagged PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}