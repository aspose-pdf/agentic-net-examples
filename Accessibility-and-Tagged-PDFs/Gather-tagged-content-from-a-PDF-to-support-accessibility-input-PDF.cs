using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // Facade API (e.g., PdfExtractor)
using Aspose.Pdf.Tagged;                // ITaggedContent and factory methods
using Aspose.Pdf.LogicalStructure;      // StructureElement and derived types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (accessibility information)
            ITaggedContent tagged = doc.TaggedContent;

            // Example: set language and title (setter methods, not properties)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Traverse the logical structure tree and output relevant information
            StructureElement root = tagged.RootElement;
            Console.WriteLine("Tagged structure tree:");
            WalkStructure(root, 0);
        }

        // Optional: use a Facade (PdfExtractor) to extract raw text as a fallback
        // This demonstrates usage of Aspose.Pdf.Facades as requested.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.ExtractText();

            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    string extractedText = reader.ReadToEnd();
                    Console.WriteLine("\n--- Extracted plain text (via PdfExtractor) ---");
                    Console.WriteLine(extractedText);
                }
            }
        }
    }

    // Recursively walk the structure tree and print element details
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string typeName = element.GetType().Name;
        string actual = element.ActualText ?? string.Empty;
        string alt    = element.AlternativeText ?? string.Empty;
        string lang   = element.Language ?? string.Empty;

        Console.WriteLine($"{indent}{typeName}:");
        if (!string.IsNullOrEmpty(actual)) Console.WriteLine($"{indent}  ActualText: {actual}");
        if (!string.IsNullOrEmpty(alt))    Console.WriteLine($"{indent}  AlternativeText: {alt}");
        if (!string.IsNullOrEmpty(lang))   Console.WriteLine($"{indent}  Language: {lang}");

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}