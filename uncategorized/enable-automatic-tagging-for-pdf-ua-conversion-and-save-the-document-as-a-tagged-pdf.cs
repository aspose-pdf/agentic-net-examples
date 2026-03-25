using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable global automatic tagging for PDF/UA conversion
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        using (Document doc = new Document(inputPath))
        {
            // Set accessible metadata (optional but recommended)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));
            tagged.SetLanguage("en-US");

            // Save the document; auto‑tagging will generate the logical structure
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}