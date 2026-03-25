using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputFolder = "input";
        const string outputFolder = "output";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please create it and add PDF files to process.");
            return;
        }

        // Enable global auto‑tagging settings
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_tagged.pdf");

            using (Document doc = new Document(pdfPath))
            {
                // Apply auto‑tagging (optional processing step)
                doc.ProcessParagraphs();

                // Save the tagged PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved: {outputPath}");
        }
    }
}
