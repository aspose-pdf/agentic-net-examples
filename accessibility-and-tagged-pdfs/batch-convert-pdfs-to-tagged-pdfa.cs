using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Input folder containing untagged PDFs
        const string inputFolder = "InputPdfs";
        // Output folder for tagged PDFs
        const string outputFolder = "TaggedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enable global auto‑tagging – applied when the document is saved
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Access the tagged‑content interface
                    ITaggedContent tagged = doc.TaggedContent;

                    // Set language and title for accessibility
                    tagged.SetLanguage("en-US");
                    tagged.SetTitle(Path.GetFileNameWithoutExtension(pdfPath));

                    // Save the auto‑tagged PDF to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);

                    Console.WriteLine($"Tagged PDF saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}