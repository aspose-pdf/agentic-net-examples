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
        const string outputPath = "output_with_span.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Span element
            SpanElement span = tagged.CreateSpanElement();

            // Set the hidden pronunciation text via ActualText
            span.ActualText = "pronunciation";

            // Optionally set visible text for the span
            span.SetText("Visible text");

            // Append the span to the root of the structure tree
            root.AppendChild(span); // AppendChild with one argument (bool defaults)

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Span ActualText to '{outputPath}'.");
    }
}