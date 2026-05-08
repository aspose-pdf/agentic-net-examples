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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language (optional)
            tagged.SetLanguage("en-US");

            // Set a document title for accessibility (optional)
            tagged.SetTitle("Accessible PDF with Paragraph Title");

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a new paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph provides a concise summary of the document content.");

            // Set the title (summary) for the paragraph element
            paragraph.Title = "Summary Paragraph";

            // Append the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}