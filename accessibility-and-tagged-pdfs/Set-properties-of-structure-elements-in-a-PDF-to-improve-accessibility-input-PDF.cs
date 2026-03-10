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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a tagged structure if none exists)
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Example: create a new paragraph element with accessibility properties
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph has been added for accessibility.");
            paragraph.AlternativeText = "Accessible paragraph describing the content.";
            paragraph.ActualText = "This is the actual text used by assistive technologies.";
            paragraph.Language = "en-US";

            // Append the paragraph to the root element
            root.AppendChild(paragraph);

            // Example: modify existing figure elements to add alternative text
            var figures = root.FindElements<FigureElement>(true);
            foreach (FigureElement fig in figures)
            {
                // Set alternative text if not already set
                if (string.IsNullOrWhiteSpace(fig.AlternativeText))
                {
                    fig.AlternativeText = "Descriptive text for the figure.";
                }

                // Optionally set language and actual text
                fig.Language = "en-US";
                fig.ActualText = "Figure description for screen readers.";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessibility-enhanced PDF saved to '{outputPath}'.");
    }
}