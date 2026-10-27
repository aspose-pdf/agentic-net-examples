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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Create a span element via the ITaggedContent factory
            SpanElement span = taggedContent.CreateSpanElement();

            // Visible text that will appear in the PDF
            span.SetText("Bonjour");

            // Hidden pronunciation text supplied via ActualText
            span.ActualText = "bɔ̃ʒuʁ";

            // Append the span to the root (single‑argument overload)
            root.AppendChild(span);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}