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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Iterate over all pages and their images
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a Figure element (represents an illustration such as an image)
                    FigureElement figure = taggedContent.CreateFigureElement();

                    // Set the ActualText attribute – this is the text a screen reader will read
                    figure.ActualText = "Description of the image for accessibility";

                    // Bind the figure element to the XImage resource on the page
                    figure.Tag(img);

                    // Attach the figure element to the structure tree
                    root.AppendChild(figure);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ActualText attributes: {outputPath}");
    }
}