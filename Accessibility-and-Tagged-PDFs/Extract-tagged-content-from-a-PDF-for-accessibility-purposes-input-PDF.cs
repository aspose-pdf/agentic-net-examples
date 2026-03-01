using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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
            // -----------------------------------------------------------------
            // 1. Extract raw text using PdfExtractor (Aspose.Pdf.Facades)
            // -----------------------------------------------------------------
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);
            extractor.ExtractText(Encoding.Unicode);
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                string rawText = Encoding.Unicode.GetString(textStream.ToArray());
                Console.WriteLine("=== Raw text extracted via PdfExtractor ===");
                Console.WriteLine(rawText);
            }

            // -----------------------------------------------------------------
            // 2. Work with tagged content (logical structure) if present
            // -----------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("The document does not contain tagged PDF content.");
                return;
            }

            // Optional: set language and title (demonstration of write‑access)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            StructureElement root = tagged.RootElement;

            Console.WriteLine("\n=== Tagged structure tree ===");
            WalkStructure(root, 0);

            // Example: list all figure elements (images) with their alternative text
            var figures = root.FindElements<FigureElement>(true);
            Console.WriteLine($"\nFound {figures.Count} figure element(s):");
            foreach (FigureElement fig in figures)
            {
                Console.WriteLine($"- AlternativeText: {fig.AlternativeText ?? "(none)"}");
            }
        }
    }

    // Recursive traversal of the logical structure tree
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string typeName = element.GetType().Name;
        string actualText = element.ActualText ?? string.Empty;
        string altText = element.AlternativeText ?? string.Empty;
        string language = element.Language ?? string.Empty;

        Console.WriteLine($"{indent}[{typeName}] ActualText=\"{actualText}\" AltText=\"{altText}\" Language=\"{language}\"");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}