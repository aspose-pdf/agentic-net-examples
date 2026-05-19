using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Suffix to append to processed files
        const string suffix = "_tagged.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Enable auto‑tagging globally (adds missing tags where possible)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Access tagged‑content API
                    ITaggedContent tagged = doc.TaggedContent;

                    // Optional: set language and title for accessibility
                    tagged.SetLanguage("en-US");
                    tagged.SetTitle(Path.GetFileNameWithoutExtension(pdfPath));

                    // Persist any changes to the structure tree
                    tagged.Save();

                    // Build output file name with the required suffix
                    string outputPath = Path.Combine(
                        inputFolder,
                        Path.GetFileNameWithoutExtension(pdfPath) + suffix);

                    // Save the (now tagged) PDF
                    doc.Save(outputPath);
                    Console.WriteLine($"Processed: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}