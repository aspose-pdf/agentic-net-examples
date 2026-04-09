using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Set document language and title (optional)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Configure automatic tagging for PDF/UA conversion
            // Use the correct PDF/UA constant (PDF_UA_1) as PdfFormat.PDF_UA does not exist
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1);
            // Use the default auto‑tagging settings and enable the feature
            options.AutoTaggingSettings = AutoTaggingSettings.Default;
            options.AutoTaggingSettings.EnableAutoTagging = true;

            // Perform the conversion (adds the tagged structure)
            doc.Convert(options);

            // Save the resulting tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
