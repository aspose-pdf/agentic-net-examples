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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language
            tagged.SetLanguage("en-US");

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Span element
            SpanElement span = tagged.CreateSpanElement();

            // Set visible text for the span (optional)
            span.SetText("example");

            // Set hidden pronunciation text using ActualText
            span.ActualText = "ɪɡˈzæmpəl";

            // Append the span to the root element
            root.AppendChild(span);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}