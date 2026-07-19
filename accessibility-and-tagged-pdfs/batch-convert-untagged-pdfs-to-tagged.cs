using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the original untagged PDFs
        const string inputFolder = "input_pdfs";
        // Folder where the newly tagged PDFs will be saved
        const string outputFolder = "tagged_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (e.g., "sample_tagged.pdf")
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_tagged.pdf");

            try
            {
                // Load the source PDF (untagged)
                using (Document doc = new Document(inputPath))
                {
                    // Prepare conversion options: target PDF/UA format with auto‑tagging enabled
                    PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1);
                    // Use the global default AutoTaggingSettings instance
                    options.AutoTaggingSettings = AutoTaggingSettings.Default;
                    // Enable the auto‑tagging process
                    options.AutoTaggingSettings.EnableAutoTagging = true;

                    // Convert the document using the specified options.
                    // This operation adds the required tagged structure.
                    doc.Convert(options);

                    // Save the newly tagged PDF to the output folder
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
