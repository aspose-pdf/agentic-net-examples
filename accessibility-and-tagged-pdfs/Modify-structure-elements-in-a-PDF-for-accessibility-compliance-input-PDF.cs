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
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Obtain the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element, set its text and language, then attach it
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible with proper structure elements.");
            paragraph.Language = "en-US";
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Create a figure element, set alternate text and language, then attach it
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample chart showing quarterly revenue.";
            figure.Language = "en-US";
            root.AppendChild(figure);

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}