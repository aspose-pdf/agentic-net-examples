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
        const string outputPath = "tagged_with_alt.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent taggedContent = doc.TaggedContent;

                // Set language and title for the document (write‑only setters)
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Iterate over all pages and their images
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Create a Figure element for the image
                        FigureElement figure = taggedContent.CreateFigureElement();

                        // Set the alternative text (maps to /ActualText in the PDF/UA sense)
                        figure.AlternativeText = "Screen‑reader description for this image";

                        // Append the figure element to the structure tree
                        root.AppendChild(figure);
                    }
                }

                // Save the modified PDF (no PreSave call required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF with image alt text saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}