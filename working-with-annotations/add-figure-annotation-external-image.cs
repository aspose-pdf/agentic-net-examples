using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_figure.pdf";
        const string imagePath = "figure.png"; // external image file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the document is tagged (create structure if missing)
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPdf));

                // Root of the tagged structure tree
                StructureElement root = tagged.RootElement;

                // Create a Figure element and set its alternative text
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Illustration showing the concept.";
                root.AppendChild(figure); // attach to the root

                // Add the external image to the first page (or any page you prefer)
                Page page = doc.Pages[1];

                // Load the image into the page resources
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // Images.Add returns the resource name (string). Retrieve the XImage via the name.
                    string imgResourceName = page.Resources.Images.Add(imgStream);
                    XImage img = page.Resources.Images[imgResourceName];
                    // Associate alternative text with the image resource
                    img.TrySetAlternativeText("Figure illustration", page);
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Figure annotation with external image saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
