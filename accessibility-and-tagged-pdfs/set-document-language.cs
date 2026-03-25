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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            // Set language for the entire document
            tagged.SetLanguage("en-US");

            // Also set language on the root structure element (optional but explicit)
            StructureElement root = tagged.RootElement;
            root.Language = "en-US";

            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with language attribute to '{outputPath}'.");
    }
}