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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a header element (level 1)
            HeaderElement header = tagged.CreateHeaderElement(1);
            header.SetText("Document Header");
            // Append the header to the root
            root.AppendChild(header);

            // First paragraph child
            ParagraphElement para1 = tagged.CreateParagraphElement();
            para1.SetText("First paragraph with larger font.");
            // Apply a specific font size using StructureTextState
            para1.StructureTextState.FontSize = 16f; // larger font
            // Append to header
            header.AppendChild(para1);

            // Second paragraph child
            ParagraphElement para2 = tagged.CreateParagraphElement();
            para2.SetText("Second paragraph with smaller font.");
            // Apply a different font size using StructureTextState
            para2.StructureTextState.FontSize = 10f; // smaller font
            // Append to header
            header.AppendChild(para2);

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
