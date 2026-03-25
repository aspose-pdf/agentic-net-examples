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
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create a paragraph to hold the span
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Create a span element
            SpanElement span = tagged.CreateSpanElement();
            // Visible text
            span.SetText("example");
            // Hidden pronunciation text (ActualText)
            span.ActualText = "ɪɡˈzæmpəl";

            // Attach span to paragraph, then paragraph to root
            paragraph.AppendChild(span);
            root.AppendChild(paragraph);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with ActualText on span to '{outputPath}'.");
    }
}