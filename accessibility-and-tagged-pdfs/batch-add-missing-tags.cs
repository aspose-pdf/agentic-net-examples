using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputFolder = "input-pdfs";
        const string outputFolder = "output-pdfs";
        const string suffix = "_tagged";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Enable global auto‑tagging (adds missing structure tags)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}{suffix}.pdf");

            try
            {
                using (Document doc = new Document(filePath))
                {
                    // Apply auto‑tagging during a conversion step (keeps PDF format)
                    var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        AutoTaggingSettings = AutoTaggingSettings.Default
                    };
                    doc.Convert(conversionOptions);

                    // Persist any changes to the tagged structure
                    doc.TaggedContent.Save();

                    doc.Save(outputPath);
                    Console.WriteLine($"Processed: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        }
    }
}