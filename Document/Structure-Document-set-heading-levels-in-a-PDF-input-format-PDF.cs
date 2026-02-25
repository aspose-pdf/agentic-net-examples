using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging and configure heading levels
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // Use default heading level detection (no int[] array)
            AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Apply auto‑tagging to the document
            doc.ProcessParagraphs();

            // Save the modified document using the PdfContentEditor facade
            PdfContentEditor editor = new PdfContentEditor(doc);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}