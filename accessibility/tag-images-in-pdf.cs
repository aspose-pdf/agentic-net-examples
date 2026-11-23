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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface. If the document is not tagged, TaggedContent will be null.
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("The PDF is not a tagged document. Tagging operations cannot be performed.");
                return;
            }

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Tag each image resource by creating a FigureElement with alternative text
            int imageIndex = 0;
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;

                    // Create a FigureElement (image tag) and set its alternative text
                    FigureElement figure = tagged.CreateFigureElement();
                    figure.AlternativeText = $"Image {imageIndex} description";

                    // Attach the figure to the root of the structure tree
                    root.AppendChild(figure);

                    // Also set alt text directly on the image resource (helps untagged PDFs)
                    img.TrySetAlternativeText($"Image {imageIndex} description", page);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}