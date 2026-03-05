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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Configure conversion options with auto‑tagging enabled
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
                convOptions.AutoTaggingSettings = AutoTaggingSettings.Default;
                convOptions.AutoTaggingSettings.EnableAutoTagging = true;

                // Perform conversion (PDF → PDF) applying the auto‑tagging settings
                doc.Convert(convOptions);

                // Set language and title via the tagged‑content API
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Save the enhanced PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}