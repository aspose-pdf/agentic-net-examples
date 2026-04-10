using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class BatchTagger
{
    static void Main()
    {
        // Input folder containing untagged PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where tagged PDFs will be saved
        const string outputFolder = @"C:\TaggedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Enable global auto‑tagging
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_tagged.pdf");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Prepare conversion options with auto‑tagging enabled
                    // NOTE: Use PdfFormat.PDF_UA_1 for PDF/UA 1.0 compliance (PdfFormat.PDF_UA does not exist)
                    PdfFormatConversionOptions convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1)
                    {
                        AutoTaggingSettings = AutoTaggingSettings.Default
                    };

                    // Convert the document (adds tagging information)
                    doc.Convert(convertOptions);

                    // Save the tagged PDF to the output folder
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
