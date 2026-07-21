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

            // Get the root element of the structure tree (no cast needed).
            StructureElement root = tagged.RootElement;

            // Create a LinkElement, set its display text, title, and hyperlink.
            LinkElement link = tagged.CreateLinkElement();
            link.SetText("Visit Example.com");               // Visible text for the link.
            link.Title = "Example Site";                     // Title attribute.
            link.Hyperlink = new WebHyperlink("https://example.com"); // External URL.

            // Append the link element to the root of the structure tree.
            root.AppendChild(link);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link saved to '{outputPath}'.");
    }
}