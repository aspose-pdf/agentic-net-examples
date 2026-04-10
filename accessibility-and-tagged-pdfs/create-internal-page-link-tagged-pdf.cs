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
        const string outputPath = "output_with_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a tagged structure (creates one if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a LinkElement – this represents a logical structure element with role /Link
            LinkElement linkElem = tagged.CreateLinkElement();

            // Create a LocalHyperlink that points to page 2 (internal page number, 1‑based)
            LocalHyperlink localLink = new LocalHyperlink
            {
                TargetPageNumber = 2 // 1‑based index of the target page
            };

            // Assign the hyperlink to the LinkElement
            linkElem.Hyperlink = localLink;

            // Optional: provide an alternate description for assistive technologies
            linkElem.AlternateDescriptions = "Go to page 2";

            // Append the LinkElement to the structure tree.
            // The overload with the boolean flag indicates that the child is a logical structure element.
            root.AppendChild(linkElem, true);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with internal link: {outputPath}");
    }
}
