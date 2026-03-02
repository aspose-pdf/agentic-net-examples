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
        const string outputPath = "modified.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: using ensures disposal)
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API (creates structure if missing)
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the tagged PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle("Modified Document");

                // Obtain the root structure element (no cast required)
                StructureElement root = tagged.RootElement;

                // Create a new paragraph element and set its text
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This paragraph was added programmatically.");

                // Append the paragraph to the root element
                root.AppendChild(paragraph);

                // Create a figure element and provide alternative text (alt description)
                FigureElement figure = tagged.CreateFigureElement();
                figure.AlternativeText = "Sample figure description.";

                // Append the figure to the root element
                root.AppendChild(figure);

                // Save the modified PDF (lifecycle: save within using block)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}