using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element
            StructureElement root = taggedContent.RootElement;

            // Create a level‑1 header element
            HeaderElement header = taggedContent.CreateHeaderElement(1);
            header.SetText("Document Header");

            // Apply text styling via the existing StructureTextState instance (read‑only property)
            header.StructureTextState.FontSize = 18;
            header.StructureTextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Attach the header to the root of the structure tree
            root.AppendChild(header);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
