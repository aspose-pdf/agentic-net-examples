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
        const string outputPath = "output_tagged.pdf";
        const string language = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set the language on the document (root element)
            tagged.SetLanguage(language);

            // Obtain the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Retrieve all descendant structure elements recursively
            var allElements = root.FindElements<StructureElement>(true);

            // Propagate the language attribute to each element
            foreach (StructureElement element in allElements)
            {
                element.Language = language;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' propagated to all elements and saved as '{outputPath}'.");
    }
}