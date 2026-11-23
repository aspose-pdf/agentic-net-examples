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
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable automatic tagging before loading the document.
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface.
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility.
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree (no cast required).
            StructureElement root = tagged.RootElement;

            // Generate a comprehensive tag report.
            Console.WriteLine("=== Tag Report ===");
            ProcessElement(root, 0);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }

    // Recursively walks the structure tree, prints details, and ensures text elements are tagged.
    static void ProcessElement(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string text = element.ActualText ?? string.Empty;
        string alt  = element.AlternativeText ?? string.Empty;

        Console.WriteLine($"{indent}{element.GetType().Name}: Text='{text}' Alt='{alt}'");

        // Example of ensuring a text element has content; replace with real logic as needed.
        if (element is ParagraphElement para && string.IsNullOrWhiteSpace(text))
        {
            para.SetText("Placeholder paragraph.");
        }
        else if (element is HeaderElement header && string.IsNullOrWhiteSpace(text))
        {
            header.SetText("Placeholder header.");
        }
        else if (element is SpanElement span && string.IsNullOrWhiteSpace(text))
        {
            span.SetText("Placeholder span.");
        }

        // Recurse into child elements.
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                ProcessElement(se, depth + 1);
        }
    }
}