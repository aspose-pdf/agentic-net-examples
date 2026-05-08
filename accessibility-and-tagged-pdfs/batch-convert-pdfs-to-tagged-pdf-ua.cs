using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs (untagged)
        const string inputFolder = "input_pdfs";

        // Folder where the tagged PDFs will be saved
        const string outputFolder = "tagged_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_tagged.pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Enable auto‑tagging globally
                    AutoTaggingSettings.Default.EnableAutoTagging = true;

                    // Prepare conversion options with auto‑tagging enabled
                    // Use the correct PDF/UA format constant
                    PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1);
                    options.AutoTaggingSettings = AutoTaggingSettings.Default;

                    // Convert the document; this adds the tagging structure
                    doc.Convert(options);

                    // Save the newly tagged PDF
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
