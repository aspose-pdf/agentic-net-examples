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
        const string outputPath = "output_pronunciation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title for accessibility (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a span element
            SpanElement span = taggedContent.CreateSpanElement();

            // Visible text that will appear in the PDF
            span.SetText("Bonjour");

            // Hidden pronunciation text (e.g., phonetic spelling)
            span.ActualText = "bɔ̃ʒuʁ";

            // Append the span to the document's structure tree
            root.AppendChild(span);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ActualText set: {outputPath}");
    }
}