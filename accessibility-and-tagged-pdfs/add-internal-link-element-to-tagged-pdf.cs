using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPage = 2;               // internal page to jump to
        const int linkPage = 1;                  // page where the link will appear
        const double llx = 100, lly = 500, urx = 300, ury = 550; // link rectangle

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Create a LinkElement (structure element with role /Link)
            LinkElement linkElement = tagged.CreateLinkElement();

            // Set the hyperlink to point to an internal page number
            LocalHyperlink localLink = new LocalHyperlink
            {
                TargetPageNumber = targetPage
            };
            linkElement.Hyperlink = localLink;

            // Create a visual link annotation on the desired page
            Page page = doc.Pages[linkPage];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            LinkAnnotation linkAnn = new LinkAnnotation(page, rect);
            linkAnn.Color = Aspose.Pdf.Color.Blue;
            // Border must be created after the annotation instance exists
            linkAnn.Border = new Border(linkAnn) { Width = 1 };

            // Bind the structure element to the annotation
            linkElement.Tag(linkAnn);

            // Append the link element to the document's structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(linkElement);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link element created and saved to '{outputPath}'.");
    }
}
