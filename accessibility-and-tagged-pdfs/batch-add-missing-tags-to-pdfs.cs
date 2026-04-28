using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class BatchTagProcessor
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";
        // Suffix to append to processed files
        const string suffix = "_tagged";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Enable global auto‑tagging (adds missing tags where possible)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF
                using (Document doc = new Document(pdfPath))
                {
                    // Access tagged content interface
                    ITaggedContent tagged = doc.TaggedContent;

                    // Optional: set language and title if needed
                    // tagged.SetLanguage("en-US");
                    // tagged.SetTitle(Path.GetFileNameWithoutExtension(pdfPath));

                    // Save any changes made to the tagged structure
                    tagged.Save();

                    // Build output file name with suffix
                    string dir = Path.GetDirectoryName(pdfPath);
                    string nameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                    string outPath = Path.Combine(dir, $"{nameWithoutExt}{suffix}.pdf");

                    // Save the updated PDF
                    doc.Save(outPath);
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