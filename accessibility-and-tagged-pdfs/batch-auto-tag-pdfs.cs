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
        const string inputFolder = @"C:\PdfFolder";
        // Suffix to add to processed files
        const string suffix = "_tagged";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Enable global auto‑tagging – this will generate tags for PDFs that lack them
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Open the PDF (using ensures proper disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Access tagged content interface
                    ITaggedContent tagged = doc.TaggedContent;

                    // If the document is already tagged, we can still invoke Save to ensure any
                    // auto‑tagging changes are persisted. No explicit PreSave() call is required.
                    // Optionally set language or title here:
                    // tagged.SetLanguage("en-US");
                    // tagged.SetTitle(Path.GetFileNameWithoutExtension(pdfPath));

                    // Save the document with the new suffix
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(pdfPath),
                        Path.GetFileNameWithoutExtension(pdfPath) + suffix + ".pdf");

                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}