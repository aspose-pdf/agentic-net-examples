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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access (or create) the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element of the tagged content tree
            StructureElement root = tagged.RootElement;

            // Create a new paragraph structure element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set a concise summary as the title of the paragraph
            paragraph.Title = "Summary of Section 1";

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph provides a concise summary of the first section.");

            // Attach the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}