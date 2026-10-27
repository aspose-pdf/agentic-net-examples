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
        const string outputFolder = "TaggedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enable global auto‑tagging – this setting is consulted during Document.Save()
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        // Optional: configure heading detection strategy
        // AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_tagged.pdf");

            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Determine whether the document is already tagged by checking TaggedContent
                bool alreadyTagged = doc.TaggedContent != null;

                if (alreadyTagged)
                {
                    // Already tagged – just copy to the output location
                    doc.Save(outputPath);
                    Console.WriteLine($"Copied already tagged PDF: {fileNameWithoutExt}");
                }
                else
                {
                    // Document is not tagged – auto‑tagging will be applied on Save()
                    // Optional: set accessibility metadata after the document becomes tagged
                    // (metadata can be set before Save when auto‑tagging is enabled)
                    ITaggedContent tagged = doc.TaggedContent; // will be created by auto‑tagging on Save
                    if (tagged != null)
                    {
                        tagged.SetLanguage("en-US");
                        tagged.SetTitle(fileNameWithoutExt);
                    }

                    doc.Save(outputPath);
                    Console.WriteLine($"Auto‑tagged PDF saved: {outputPath}");
                }
            }
        }
    }
}
