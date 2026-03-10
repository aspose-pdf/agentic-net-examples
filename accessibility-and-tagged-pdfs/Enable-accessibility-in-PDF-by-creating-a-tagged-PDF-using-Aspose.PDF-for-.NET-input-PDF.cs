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
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element with accessible text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible using Aspose.Pdf.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Optionally add a figure element for the first image on the first page
            if (doc.Pages.Count > 0 && doc.Pages[1].Resources.Images.Count > 0)
            {
                // Create a figure element
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample image description.";

                // Associate the image resource with the figure
                // Retrieve the first image name from the page's image collection
                string imageName = null;
                foreach (XImage img in doc.Pages[1].Resources.Images)
                {
                    imageName = img.Name; // May be null; use first available name
                    break;
                }

                if (!string.IsNullOrEmpty(imageName))
                {
                    figure.SetImage(imageName);
                }

                root.AppendChild(figure);
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}