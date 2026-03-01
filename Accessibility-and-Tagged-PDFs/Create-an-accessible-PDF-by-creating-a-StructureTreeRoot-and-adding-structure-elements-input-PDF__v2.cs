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
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Obtain the StructTreeRootElement (root of the logical structure)
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            // Create a paragraph element and set its text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible with a structure tree.");
            // Append the paragraph to the structure tree root
            structRoot.AppendChild(paragraph);

            // Create a figure element (e.g., for an image) and set alternative text
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample image description.";
            // Append the figure to the structure tree root
            structRoot.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}