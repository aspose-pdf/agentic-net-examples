using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        string inputFolder = "input-pdfs";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Enable auto‑tagging globally for all documents processed
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    ITaggedContent tagged = doc.TaggedContent;
                    // Optional: set document language for accessibility
                    tagged.SetLanguage("en-US");

                    // Persist any tagged‑content changes
                    tagged.Save();

                    string outputPath = Path.Combine(
                        inputFolder,
                        Path.GetFileNameWithoutExtension(pdfPath) + "_tagged.pdf");

                    doc.Save(outputPath);
                    Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {Path.GetFileName(outputPath)}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}