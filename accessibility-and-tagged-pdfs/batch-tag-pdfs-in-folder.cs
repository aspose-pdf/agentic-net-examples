using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class BatchTagger
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where tagged PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file name with "_tagged" suffix before the extension
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_tagged.pdf");

            // Load the PDF, enable auto‑tagging, and save the result
            using (Document doc = new Document(pdfPath))
            {
                // Enable automatic tagging for missing structure elements
                AutoTaggingSettings.Default.EnableAutoTagging = true;

                // Optional: set language and title for accessibility
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle(fileName);

                // Save the tagged PDF; no PreSave() call is required
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved: {outputPath}");
        }
    }
}