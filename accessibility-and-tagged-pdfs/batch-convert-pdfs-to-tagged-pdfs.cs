using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Tagged;               // For ITaggedContent if needed (not used here)

class Program
{
    static void Main()
    {
        // Input folder containing untagged PDFs
        const string inputFolder = "input_pdfs";
        // Output folder where tagged PDFs will be written
        const string outputFolder = "tagged_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enable auto‑tagging globally (static settings)
        // This follows the rule: AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
            string destPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_tagged.pdf");

            try
            {
                // Load the source PDF (using the provided lifecycle rule)
                using (Document doc = new Document(sourcePath))
                {
                    // Prepare conversion options that carry the auto‑tagging settings.
                    // PdfFormatConversionOptions is used for format conversion; we convert to PDF/A‑1B
                    // to force a full conversion pass where auto‑tagging is applied.
                    PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        AutoTaggingSettings = AutoTaggingSettings.Default
                    };

                    // Perform the conversion. The document is now tagged according to the settings.
                    doc.Convert(convOptions);

                    // Save the tagged PDF to the output location (using the provided save rule)
                    doc.Save(destPath);
                }

                Console.WriteLine($"Tagged PDF created: {destPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }
}