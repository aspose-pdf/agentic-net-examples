using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable automatic tagging globally
        // This setting causes Aspose.Pdf to generate tagged content when the document is saved.
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Optional: set language and title for the tagged PDF
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the document – auto‑tagging is applied during the save operation
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}