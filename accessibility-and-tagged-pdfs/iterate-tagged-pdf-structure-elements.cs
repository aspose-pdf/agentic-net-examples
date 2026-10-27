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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content; if the document is not tagged, TaggedContent will be null
            ITaggedContent taggedContent = doc.TaggedContent;
            if (taggedContent == null)
            {
                Console.WriteLine("The document is not a tagged PDF.");
                return;
            }

            // Root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Recursively find all structure elements
            var allElements = root.FindElements<StructureElement>(true);

            Console.WriteLine("Structure Elements:");
            foreach (StructureElement element in allElements)
            {
                // Tag name (type of the element)
                string tagName = element.GetType().Name;

                // Language property (if set)
                string language = element.Language ?? "(none)";

                // Title / actual text (if any)
                string title = element.ActualText ?? "(none)";

                Console.WriteLine($"Tag: {tagName}, Language: {language}, Title: {title}");
            }
        }
    }
}