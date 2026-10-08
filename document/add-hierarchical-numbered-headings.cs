using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "numbered_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Enable tagging and set basic metadata
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree – use dynamic to avoid compile‑time dependency on StructureElement
            dynamic root = tagged.RootElement;

            // Create a level‑1 heading with decimal (Arabic) numbering
            var h1 = new Heading(1)
            {
                Text = "Chapter 1: Introduction",
                IsAutoSequence = true,               // enable automatic numbering
                Style = NumberingStyle.NumeralsArabic // decimal numbering
            };
            root.AppendChild(h1);

            // Create a level‑2 heading with decimal (Arabic) numbering
            var h2 = new Heading(2)
            {
                Text = "Section 1.1: Overview",
                IsAutoSequence = true,
                Style = NumberingStyle.NumeralsArabic
            };
            root.AppendChild(h2);

            // Add visible text for the headings on the first page
            Page page = doc.Pages[1];

            TextFragment tf1 = new TextFragment("Chapter 1: Introduction");
            tf1.TextState.FontSize = 20;
            tf1.TextState.Font = FontRepository.FindFont("Arial");
            tf1.Position = new Position(50, 750);
            page.Paragraphs.Add(tf1);

            TextFragment tf2 = new TextFragment("Section 1.1: Overview");
            tf2.TextState.FontSize = 16;
            tf2.TextState.Font = FontRepository.FindFont("Arial");
            tf2.Position = new Position(70, 720);
            page.Paragraphs.Add(tf2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}