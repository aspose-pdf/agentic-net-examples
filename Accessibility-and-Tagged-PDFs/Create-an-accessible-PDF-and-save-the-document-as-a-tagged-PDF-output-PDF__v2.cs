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
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast required)
                StructureElement root = tagged.RootElement;

                // Create a paragraph element with accessible text
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been made accessible using Aspose.Pdf.");
                root.AppendChild(paragraph); // Attach to the root

                // Create a figure element with alternative text (example illustration)
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample illustration showing data flow.";
                root.AppendChild(figure);

                // Save the tagged content changes into the document
                tagged.Save();

                // Persist the modified PDF to disk
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