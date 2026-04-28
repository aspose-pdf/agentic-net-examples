using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, FigureElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_tagged.pdf";

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

            // Optional: set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Iterate over all pages and their image resources
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Set alternative text directly on the XImage (helps screen readers)
                    const string altText = "Descriptive text for the image";
                    img.TrySetAlternativeText(altText, page);

                    // Create a FigureElement that represents the image in the logical structure
                    FigureElement figure = taggedContent.CreateFigureElement();

                    // Set the ActualText attribute (used by assistive technologies)
                    figure.ActualText = altText;

                    // Bind the FigureElement to the XImage resource
                    figure.Tag(img);

                    // Attach the figure to the document's structure tree
                    root.AppendChild(figure);
                }
            }

            // Save the modified PDF (PDF output is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}