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
        const string actualText = "Descriptive alternative text for the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Iterate over all pages and their images
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a Figure element for the image
                    FigureElement figure = tagged.CreateFigureElement();
                    // Set the ActualText attribute (used by screen readers)
                    figure.ActualText = actualText;
                    // Bind the figure element to the XImage resource
                    figure.Tag(img);
                    // Append the figure element to the structure tree
                    root.AppendChild(figure);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ActualText on images: {outputPath}");
    }
}