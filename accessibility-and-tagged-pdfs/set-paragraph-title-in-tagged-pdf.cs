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

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access the tagged content interface
            Aspose.Pdf.Tagged.ITaggedContent taggedContent = doc.TaggedContent;

            // Set document language and title (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            Aspose.Pdf.LogicalStructure.StructureElement root = taggedContent.RootElement;

            // Create a new paragraph element
            Aspose.Pdf.LogicalStructure.ParagraphElement paragraph = taggedContent.CreateParagraphElement();

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph provides a concise summary of the document.");

            // Set the Title attribute of the paragraph (used for accessibility)
            paragraph.Title = "Summary Paragraph";

            // Append the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}