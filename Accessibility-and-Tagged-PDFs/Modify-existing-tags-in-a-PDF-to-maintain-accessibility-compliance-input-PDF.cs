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
        const string outputPath = "tagged_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Update document-level language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Example 1: Update all paragraph elements
            var paragraphs = root.FindElements<ParagraphElement>(true);
            foreach (ParagraphElement para in paragraphs)
            {
                // Set the displayed text (if needed)
                // para.SetText("Updated paragraph text"); // uncomment to replace text

                // Ensure the language attribute is set
                para.Language = "en-US";
            }

            // Example 2: Update all figure elements (e.g., images) with alternate text
            var figures = root.FindElements<FigureElement>(true);
            foreach (FigureElement fig in figures)
            {
                // Provide a generic description if none exists
                if (string.IsNullOrWhiteSpace(fig.AlternativeText))
                {
                    fig.AlternativeText = "Descriptive alternate text for image";
                }

                // Set language for the figure element
                fig.Language = "en-US";
            }

            // Example 3: Update all header elements (H1‑H6)
            var headers = root.FindElements<HeaderElement>(true);
            foreach (HeaderElement hdr in headers)
            {
                // Ensure language attribute is present
                hdr.Language = "en-US";
            }

            // Persist changes to the tagged structure
            tagged.Save();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}