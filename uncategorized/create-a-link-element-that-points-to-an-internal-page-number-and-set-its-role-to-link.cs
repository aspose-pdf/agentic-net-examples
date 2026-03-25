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
            StructureElement root = tagged.RootElement;

            // Create a Link element (role /Link)
            LinkElement link = tagged.CreateLinkElement();

            // Set the internal page target (e.g., page 2)
            LocalHyperlink localLink = new LocalHyperlink
            {
                TargetPageNumber = 2
            };
            link.Hyperlink = localLink;

            // Optional: provide alternate text for accessibility
            link.AlternativeText = "Navigate to page 2";

            // Attach the link element to the document's structure tree
            root.AppendChild(link);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Linked PDF saved to '{outputPath}'.");
    }
}