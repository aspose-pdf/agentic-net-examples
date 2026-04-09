using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class BatchTagger
{
    static void Main()
    {
        // Folder containing the source PDFs (untagged)
        const string inputFolder = @"C:\InputPdfs";
        // Folder where the tagged PDFs will be saved
        const string outputFolder = @"C:\TaggedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        // Enable the global auto‑tagging setting (optional, can be set per file as well)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_tagged.pdf");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Set document language and title (optional but recommended for accessibility)
                    ITaggedContent tagged = doc.TaggedContent;
                    tagged.SetLanguage("en-US");
                    tagged.SetTitle(fileName);

                    // Prepare conversion options with auto‑tagging enabled
                    PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF);
                    convOptions.AutoTaggingSettings = AutoTaggingSettings.Default;
                    convOptions.AutoTaggingSettings.EnableAutoTagging = true;

                    // Perform the conversion – this applies auto‑tagging to the document
                    doc.Convert(convOptions);

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

        Console.WriteLine("Batch tagging completed.");
    }
}