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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Iterate over all pages and their image resources
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a new Figure element (represents an image in the structure)
                    FigureElement figure = taggedContent.CreateFigureElement();

                    // Set the ActualText attribute – this is the text a screen reader will read
                    figure.ActualText = "Descriptive alternative text for the image.";

                    // Bind the figure element to the existing XImage on the page
                    figure.Tag(img);

                    // Append the figure element to the root of the structure tree
                    root.AppendChild(figure);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ActualText attributes: {outputPath}");
    }
}