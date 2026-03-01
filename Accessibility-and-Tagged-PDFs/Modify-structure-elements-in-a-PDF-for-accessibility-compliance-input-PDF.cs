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
        const string outputPath = "output_accessible.pdf";

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
                // Access tagged content
                ITaggedContent tagged = doc.TaggedContent;

                // Set document language and title
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast required)
                StructureElement root = tagged.RootElement;

                // Add a new paragraph describing the accessibility improvements
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This document has been processed to improve accessibility compliance.");
                root.AppendChild(paragraph); // AppendChild with a single argument

                // Set alternative text for all existing figure elements
                var figures = root.FindElements<FigureElement>(true);
                foreach (FigureElement fig in figures)
                {
                    fig.AlternativeText = "Descriptive alternate text for the figure.";
                }

                // Ensure all paragraph elements have the correct language attribute
                var paragraphs = root.FindElements<ParagraphElement>(true);
                foreach (ParagraphElement p in paragraphs)
                {
                    p.Language = "en-US";
                }

                // Save the modified PDF
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