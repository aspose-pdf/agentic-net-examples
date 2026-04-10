using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_actualtext.pdf";
        const string altText    = "Descriptive text for screen readers";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Iterate over all pages and their images
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Optionally set the image's alternative text (useful for untagged PDFs)
                    img.TrySetAlternativeText(altText, page);

                    // Create a FigureElement to represent the image in the structure tree
                    FigureElement figure = taggedContent.CreateFigureElement();

                    // Set the ActualText (replacement text for screen readers)
                    figure.ActualText = altText;

                    // Also set AlternativeText for completeness
                    figure.AlternativeText = altText;

                    // Bind the FigureElement to the XImage resource
                    figure.Tag(img);

                    // Append the figure to the root of the structure tree
                    root.AppendChild(figure);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ActualText attributes: {outputPath}");
    }
}