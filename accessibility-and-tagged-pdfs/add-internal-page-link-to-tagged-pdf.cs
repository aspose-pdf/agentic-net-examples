using System;
using System.IO;
using Aspose.Pdf;                     // Document, LocalHyperlink
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;     // LinkElement, StructureElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPage    = 2; // internal page number to link to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Create a LinkElement (its role is automatically /Link)
            LinkElement linkElem = tagged.CreateLinkElement();

            // Build a LocalHyperlink that points to the desired page
            LocalHyperlink localLink = new LocalHyperlink
            {
                TargetPageNumber = targetPage
            };

            // Assign the hyperlink to the LinkElement
            linkElem.Hyperlink = localLink;

            // Optional: provide alternate text for accessibility
            linkElem.AlternativeText = $"Link to page {targetPage}";

            // Append the link element to the document's structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(linkElem); // AppendChild with one argument (default bool)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link element added; saved to '{outputPath}'.");
    }
}