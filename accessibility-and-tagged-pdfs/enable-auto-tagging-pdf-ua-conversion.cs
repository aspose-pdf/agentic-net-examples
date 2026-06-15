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

        // Enable global auto‑tagging (optional, ensures default settings are on)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Prepare conversion options for PDF/UA format with auto‑tagging enabled
            PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1);
            convOpts.AutoTaggingSettings = new AutoTaggingSettings
            {
                EnableAutoTagging = true
            };

            // Perform the conversion – this applies automatic tagging
            doc.Convert(convOpts);

            // Set document‑level metadata for the tagged PDF
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the resulting tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}