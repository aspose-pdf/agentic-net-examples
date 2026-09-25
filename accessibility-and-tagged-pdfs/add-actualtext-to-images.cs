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
        const string outputPath = "output.pdf";
        const string altText    = "Description of the image for screen readers.";
        const string actualText = "Image caption or actual text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent tagged = doc.TaggedContent;
                // Optional: set language and title for the document
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root of the structure tree (no cast needed)
                StructureElement root = tagged.RootElement;

                // Create a Figure element representing the image
                FigureElement figure = tagged.CreateFigureElement();
                // Set the alternate text (used by assistive technologies)
                figure.AlternativeText = altText;
                // Set the ActualText attribute (explicit textual representation)
                figure.ActualText = actualText;

                // Attach the figure element to the document structure
                root.AppendChild(figure);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with image ActualText saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}