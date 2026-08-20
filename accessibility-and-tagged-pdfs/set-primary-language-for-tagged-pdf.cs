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
        const string outputPath = "output_with_language.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set the primary language for the whole document
            taggedContent.SetLanguage("en-US");

            // Optionally, also set the language on the root structure element
            StructureElement root = taggedContent.RootElement;
            root.Language = "en-US";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with language attribute: {outputPath}");
    }
}