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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates one if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the whole document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a LinkElement via the ITaggedContent factory
            LinkElement link = tagged.CreateLinkElement();

            // Set the visible text of the link
            link.SetText("Visit Aspose.Pdf");

            // Assign the external URL using WebHyperlink
            link.Hyperlink = new WebHyperlink("https://www.aspose.com/pdf");

            // Define the Title attribute (appears as tooltip in PDF viewers)
            link.Title = "Aspose.Pdf product page";

            // Append the link element to the document's structure tree
            root.AppendChild(link);   // AppendChild with one argument (bool defaults)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link saved to '{outputPath}'.");
    }
}