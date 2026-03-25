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
        const string actualText = "Image description for screen readers";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a FigureElement representing the image
                    FigureElement figure = tagged.CreateFigureElement();
                    // Set the ActualText attribute (alternative description)
                    figure.ActualText = actualText;
                    // Bind the FigureElement to the XImage resource
                    figure.Tag(img);
                    // Attach the figure to the structure tree
                    root.AppendChild(figure);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ActualText for images: {outputPath}");
    }
}