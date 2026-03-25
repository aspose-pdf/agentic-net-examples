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
        const int targetPage    = 2; // internal page number (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a Link element (role /Link is inherent)
            LinkElement linkElement = tagged.CreateLinkElement();

            // Set the hyperlink to point to an internal page
            LocalHyperlink hyperlink = new LocalHyperlink();
            hyperlink.TargetPageNumber = targetPage; // internal page number
            linkElement.Hyperlink = hyperlink;

            // Attach the link element to the document structure
            root.AppendChild(linkElement);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}