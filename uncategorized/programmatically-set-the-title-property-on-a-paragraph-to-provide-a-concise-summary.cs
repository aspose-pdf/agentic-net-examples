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

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            // Optional: set document language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element, set its text and title (summary)
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph provides a concise summary of the document.");
            paragraph.Title = "Summary Paragraph"; // Title property on StructureElement

            // Attach the paragraph to the root
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}