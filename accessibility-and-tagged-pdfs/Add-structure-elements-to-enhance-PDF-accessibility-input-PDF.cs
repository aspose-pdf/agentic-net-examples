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

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the tagged PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast required)
                StructureElement root = tagged.RootElement;

                // ----- Add a heading element -----
                HeaderElement heading = tagged.CreateHeaderElement(1);
                heading.SetText("Document Title");
                heading.Language = "en-US"; // use Language property (not Lang)
                root.AppendChild(heading); // AppendChild with one argument

                // ----- Add a paragraph element -----
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been enhanced with accessibility tags.");
                paragraph.Language = "en-US";
                root.AppendChild(paragraph);

                // ----- Add a figure element with alternate text -----
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample diagram showing process flow.";
                figure.Language = "en-US";
                root.AppendChild(figure);

                // Save the modified PDF (lifecycle rule: use Document.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}