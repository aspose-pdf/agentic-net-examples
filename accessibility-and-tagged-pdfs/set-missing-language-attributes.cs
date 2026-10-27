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
        const string defaultLang = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine whether the PDF is tagged by checking the TaggedContent property
            ITaggedContent taggedContent = doc.TaggedContent;
            if (taggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure elements to process.");
                // Save the original document unchanged
                doc.Save(outputPath);
                return;
            }

            // Optionally set the document‑level language (used when elements inherit it)
            taggedContent.SetLanguage(defaultLang);

            // Root of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Retrieve every structure element in the tree (recursive)
            var allElements = root.FindElements<StructureElement>(true);

            // Assign the default language to elements that lack an explicit setting
            foreach (StructureElement element in allElements)
            {
                if (string.IsNullOrEmpty(element.Language))
                {
                    element.Language = defaultLang;
                }
            }

            // Persist the changes
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
