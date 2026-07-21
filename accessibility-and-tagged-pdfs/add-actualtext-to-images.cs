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
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document has a root structure element
            StructureElement root = tagged.RootElement;

            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over each image resource on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a FigureElement (illustration structure element)
                    FigureElement figure = tagged.CreateFigureElement();

                    // Set the ActualText attribute – this is the text a screen reader will read
                    figure.ActualText = "Description of the image for accessibility";

                    // Bind the FigureElement to the XImage instance
                    figure.Tag(img);

                    // Append the figure element to the document's structure tree
                    root.AppendChild(figure);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ActualText attributes: {outputPath}");
    }
}