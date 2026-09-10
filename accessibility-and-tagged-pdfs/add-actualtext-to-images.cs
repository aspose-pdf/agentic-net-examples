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
        const string altText    = "Screen‑reader description of the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            // Ensure the document is marked as tagged (no explicit setter needed)
            // Set language for accessibility (optional)
            tagged.SetLanguage("en-US");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Iterate over all pages and their image resources
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a FigureElement (illustration structure element)
                    FigureElement figure = tagged.CreateFigureElement();

                    // Set the ActualText attribute – this is the text a screen reader will read
                    figure.ActualText = altText;

                    // Bind the structure element to the actual XImage on the page
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