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
        const string language = "en-US";
        const string title = "Accessible Document";

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
                // Access tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for accessibility metadata
                tagged.SetLanguage(language);
                tagged.SetTitle(title);

                // Get the root structure element (no cast needed)
                StructureElement root = tagged.RootElement;

                // Add a paragraph element describing the accessibility improvements
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been enhanced for accessibility.");
                root.AppendChild(paragraph); // AppendChild with one argument

                // Add alternative text to all existing images in the document
                foreach (Page page in doc.Pages) // Pages are 1‑based, iteration works correctly
                {
                    foreach (XImage img in page.Resources.Images) // Direct foreach, not a dictionary
                    {
                        img.TrySetAlternativeText("Descriptive alt text for image.", page);
                    }
                }

                // Save the modified PDF (no PreSave required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Accessibility enhancements saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}