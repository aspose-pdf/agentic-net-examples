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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent tagged = doc.TaggedContent;

                // Get the root element of the structure tree
                StructureElement root = tagged.RootElement;

                // Create a Span element, set visible text and hidden ActualText
                SpanElement span = tagged.CreateSpanElement();
                span.SetText("Visible text");               // optional visible content
                span.ActualText = "pronunciation text";    // hidden pronunciation text

                // Attach the span to the document's structure tree
                root.AppendChild(span);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}