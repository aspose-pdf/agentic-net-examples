using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;                 // ITaggedContent
using Aspose.Pdf.LogicalStructure;      // StructureElement, LinkElement
using Aspose.Pdf;                        // WebHyperlink

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const string url        = "https://www.example.com";
        const string linkTitle  = "Example Site";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: ensure the document is marked as tagged (no setter needed)
            // The RootElement is a StructureElement; no cast required.
            StructureElement root = tagged.RootElement;

            // Create a LinkElement (structure element representing a link)
            LinkElement linkElem = tagged.CreateLinkElement();

            // Set the visible text of the link (what screen readers will read)
            linkElem.SetText("Visit Example Site");

            // Assign the title attribute (appears as tooltip in some viewers)
            linkElem.Title = linkTitle;

            // Create a WebHyperlink pointing to the external URL
            WebHyperlink webLink = new WebHyperlink(url);

            // Associate the hyperlink with the LinkElement
            linkElem.Hyperlink = webLink;

            // Append the link element to the document's structure tree
            root.AppendChild(linkElem);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with external link: {outputPath}");
    }
}