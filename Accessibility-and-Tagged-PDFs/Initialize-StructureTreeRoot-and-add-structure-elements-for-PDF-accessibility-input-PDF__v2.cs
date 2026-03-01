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
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root StructureElement (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element, set its text, and attach it to the root
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible using Aspose.Pdf.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Create a figure element, set alternative text, and attach it to the root
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample figure describing the chart.";
            root.AppendChild(figure);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}