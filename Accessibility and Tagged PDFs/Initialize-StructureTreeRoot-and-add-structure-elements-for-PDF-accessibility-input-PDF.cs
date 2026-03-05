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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the tagged PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast needed)
                StructureElement root = tagged.RootElement;

                // Create a paragraph element and set its text
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been made accessible using Aspose.Pdf.");
                // Append the paragraph to the root element
                root.AppendChild(paragraph);

                // Create a figure element (e.g., for an image) and set alternative text
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample image description.";
                // Append the figure to the root element
                root.AppendChild(figure);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}