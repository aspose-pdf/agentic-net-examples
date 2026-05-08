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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (no special load options needed for a regular PDF)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document has a structure tree
            StructureElement root = tagged.RootElement;

            // Create a LinkElement (its role is already /Link)
            LinkElement linkElem = tagged.CreateLinkElement();

            // Create a LocalHyperlink that points to page 2 (page numbers are 1‑based)
            LocalHyperlink localLink = new LocalHyperlink
            {
                TargetPageNumber = 2
            };

            // Associate the hyperlink with the LinkElement
            linkElem.Hyperlink = localLink;

            // Append the LinkElement to the root of the structure tree
            root.AppendChild(linkElem);

            // (Optional) Add a visible link annotation on the target page for testing
            // Define a rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Create the annotation on page 1 (or any page you prefer)
            LinkAnnotation annotation = new LinkAnnotation(doc.Pages[1], rect)
            {
                // Use a GoToAction to the same page number for visual consistency
                Action = new GoToAction(doc.Pages[2])
            };
            doc.Pages[1].Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}