using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_language_set.pdf";
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using rule: document-disposal-with-using)
            using (Document doc = new Document(inputPath))
            {
                // Set the natural language for the entire PDF (access via ITaggedContent)
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage(language);

                // Save the updated PDF (using rule: save-to-non-pdf-always-use-save-options is not needed here)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with language '{language}' to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}