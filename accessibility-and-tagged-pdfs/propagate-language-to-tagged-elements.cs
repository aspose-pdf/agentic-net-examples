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
        const string outputPath = "output.pdf";
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set the document language (root level)
            tagged.SetLanguage(language);

            // The root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Ensure the root element also has the Language property set
            root.Language = language;

            // Recursively find all structure elements under the root
            // FindElements<T>(true) performs a deep search
            var allElements = root.FindElements<StructureElement>(true);

            // Propagate the language to each child element
            foreach (StructureElement element in allElements)
            {
                element.Language = language;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' propagated to all structure elements and saved to '{outputPath}'.");
    }
}