using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where the tagged PDFs will be written
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enable automatic tagging for all documents processed in this run
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF – the using block guarantees deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Access the tagged‑content API
                    ITaggedContent tagged = doc.TaggedContent;

                    // Apply any pending auto‑tagging changes
                    tagged.Save();

                    // Build the output file name with a "_tagged" suffix
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(pdfPath) + "_tagged.pdf");

                    // Save the updated PDF
                    doc.Save(outputPath);

                    Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}