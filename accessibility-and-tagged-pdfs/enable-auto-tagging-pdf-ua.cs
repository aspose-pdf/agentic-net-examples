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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set document language and title via the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Prepare conversion options with automatic tagging enabled
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1);
            // Use the default auto‑tagging settings and ensure it is enabled
            options.AutoTaggingSettings = AutoTaggingSettings.Default;
            options.AutoTaggingSettings.EnableAutoTagging = true;

            // Perform the conversion; this runs the auto‑tagging process
            doc.Convert(options);

            // Save the resulting tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
