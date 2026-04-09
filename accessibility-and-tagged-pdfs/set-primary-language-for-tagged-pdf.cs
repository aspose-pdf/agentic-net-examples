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
        const string outputPath = "output_tagged.pdf";
        const string language   = "en-US";

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
                // Obtain the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set the primary language for the entire document
                tagged.SetLanguage(language);

                // Also set the language on the root structure element (optional, reinforces the setting)
                StructureElement root = tagged.RootElement;
                root.Language = language;

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved with language '{language}' to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}