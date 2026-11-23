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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF contains a logical structure (tagged content)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged; no logical structure to modify.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Locate the first FigureElement (image tag) in the structure tree
            FigureElement figure = null;
            var figures = tagged.RootElement.FindElements<FigureElement>(true);
            if (figures.Count > 0)
                figure = figures[0];

            if (figure == null)
            {
                Console.WriteLine("No image (FigureElement) found in the document.");
                return;
            }

            // Create a caption paragraph element
            ParagraphElement caption = tagged.CreateParagraphElement();
            caption.SetText("Figure 1: Sample image description.");

            // Insert the caption as a child of the image tag (as the first child)
            figure.InsertChild(caption, 0);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with caption: {outputPath}");
    }
}