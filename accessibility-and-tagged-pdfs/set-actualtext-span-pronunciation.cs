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

        // Open the PDF document (lifecycle managed by using)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set the document language
            tagged.SetLanguage("en-US");

            // Create a Span element (inline text structure)
            SpanElement span = tagged.CreateSpanElement();

            // Set the visible text of the span (if needed)
            span.SetText("example");

            // Supply hidden pronunciation text via ActualText
            span.ActualText = "ɪɡˈzæmpəl";

            // Attach the span to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(span); // AppendChild with one argument (default bool)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}