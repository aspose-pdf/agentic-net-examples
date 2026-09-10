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
        const string outputPath = "output_with_paragraph_title.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optionally set document language and title (metadata)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Create a new paragraph structure element
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph provides a concise summary of the document.");

            // Set the Title property on the paragraph (used for accessibility)
            paragraph.Title = "Summary Paragraph";

            // Append the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with paragraph title to '{outputPath}'.");
    }
}