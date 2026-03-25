using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF (can be untagged)
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a tagged structure (Aspose creates one if missing)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element, set its visible text and a concise title
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph provides a brief summary of the document content.");
            paragraph.Title = "Summary"; // concise title for the paragraph

            // Attach the paragraph to the structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Paragraph title set and saved to '{outputPath}'.");
    }
}