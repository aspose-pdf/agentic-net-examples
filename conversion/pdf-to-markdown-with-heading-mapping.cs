using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.md";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access tagged content if the PDF is tagged
            ITaggedContent tagged = doc.TaggedContent;

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                if (tagged != null && tagged.RootElement != null)
                {
                    // Recursively write structure elements as Markdown
                    WriteElement(tagged.RootElement, writer);
                }
                else
                {
                    // Fallback: extract plain text when no tagging is present
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    writer.WriteLine(absorber.Text);
                }
            }
        }

        Console.WriteLine($"Markdown saved to '{outputPath}'.");
    }

    // Recursively writes a structure element and its children to the Markdown file
    static void WriteElement(StructureElement element, StreamWriter writer)
    {
        // Handle header elements – map PDF heading levels to Markdown '#'
        if (element is HeaderElement header)
        {
            int level = 1; // default level

            // Attempt to read the Level property via reflection (if it exists)
            PropertyInfo levelProp = header.GetType().GetProperty("Level");
            if (levelProp != null && levelProp.PropertyType == typeof(int))
            {
                object val = levelProp.GetValue(header);
                if (val is int lvl && lvl > 0)
                    level = lvl;
            }

            // Clamp level to a maximum of 6 (Markdown supports up to ######)
            level = Math.Min(level, 6);
            string hashes = new string('#', level);
            string text   = header.ActualText ?? string.Empty;
            writer.WriteLine($"{hashes} {text}");
        }
        // Paragraph elements – write their text as a normal paragraph
        else if (element is ParagraphElement paragraph)
        {
            string text = paragraph.ActualText ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(text))
                writer.WriteLine(text);
        }
        // Any other element – write its actual text if available
        else
        {
            string text = element.ActualText ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(text))
                writer.WriteLine(text);
        }

        // Recurse into child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WriteElement(se, writer);
        }
    }
}