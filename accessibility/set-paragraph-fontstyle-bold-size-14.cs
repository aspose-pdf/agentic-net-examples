using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // for FontStyles

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a tagged PDF if not already tagged)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root structure element
            StructureElement root = taggedContent.RootElement;

            // Create a new paragraph structure element
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();
            paragraph.SetText("This paragraph is bold and size 14.");

            // Apply text styling via the read‑only StructureTextState property
            paragraph.StructureTextState.FontSize = 14f;
            paragraph.StructureTextState.FontStyle = FontStyles.Bold;

            // Append the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
