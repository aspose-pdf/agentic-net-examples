using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string language = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content and set the document language
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage(language);

                // Optionally set the language on the root structure element
                StructureElement root = tagged.RootElement;
                root.Language = language;

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Language '{language}' applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}