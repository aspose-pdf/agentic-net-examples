using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;                 // ITaggedContent
using Aspose.Pdf.LogicalStructure;      // StructureElement, LinkElement
using Aspose.Pdf;                       // WebHyperlink

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const string url        = "https://www.example.com";
        const string title      = "Example Site";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify its tagged structure, and save.
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API.
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language and title for accessibility.
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed).
            StructureElement root = tagged.RootElement;

            // Create a LinkElement via the ITaggedContent factory.
            LinkElement linkElem = tagged.CreateLinkElement();

            // Set the visible text of the link (what screen readers will read).
            linkElem.SetText("Visit Example Site");

            // Assign the external URL using WebHyperlink.
            linkElem.Hyperlink = new WebHyperlink(url);

            // Define the Title attribute (tooltip / additional description).
            linkElem.Title = title;

            // Append the link element to the document's structure tree.
            root.AppendChild(linkElem);   // bool parameter omitted (default true)

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link saved to '{outputPath}'.");
    }
}