using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const int    targetPage = 2; // internal page number to jump to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document has a structure tree
            StructureElement root = tagged.RootElement;

            // Create a Link structure element
            LinkElement linkElem = tagged.CreateLinkElement();

            // Create a local hyperlink that points to the desired page
            LocalHyperlink hyperlink = new LocalHyperlink
            {
                TargetPageNumber = targetPage
            };

            // Assign the hyperlink to the Link element
            linkElem.Hyperlink = hyperlink;

            // Optionally set visible text for the link (e.g., "Go to page 2")
            linkElem.SetText($"Go to page {targetPage}");

            // Append the Link element to the root of the structure tree
            root.AppendChild(linkElem);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with internal link: {outputPath}");
    }
}