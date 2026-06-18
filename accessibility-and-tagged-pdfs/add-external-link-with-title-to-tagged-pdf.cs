using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, LinkElement
using Aspose.Pdf;                     // WebHyperlink

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
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document is tagged (if not, tagging will be created automatically)
            StructureElement root = tagged.RootElement;

            // Create a LinkElement in the logical structure
            LinkElement linkElement = tagged.CreateLinkElement();

            // Set the visible text for the link (optional, but useful for accessibility)
            linkElement.SetText("Visit Example Site");

            // Assign the title attribute (appears as tooltip in PDF viewers)
            linkElement.Title = linkTitle;

            // Create a WebHyperlink pointing to the external URL and assign it
            WebHyperlink webLink = new WebHyperlink(url);
            linkElement.Hyperlink = webLink;

            // Attach the LinkElement to the root of the structure tree
            root.AppendChild(linkElement);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with external link: '{outputPath}'.");
    }
}