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
        const string outputPath = "output_with_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface (creates a structure tree if missing)
                ITaggedContent taggedContent = doc.TaggedContent;

                // Set language and title for accessibility metadata
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root element of the logical structure tree
                StructureElement root = taggedContent.RootElement;

                // Add a level‑1 heading
                HeaderElement heading1 = taggedContent.CreateHeaderElement(1);
                heading1.SetText("Chapter 1: Introduction");
                root.AppendChild(heading1); // AppendChild with one argument (default bool)

                // Add a level‑2 heading
                HeaderElement heading2 = taggedContent.CreateHeaderElement(2);
                heading2.SetText("Section 1.1: Background");
                root.AppendChild(heading2);

                // Add another level‑2 heading
                HeaderElement heading3 = taggedContent.CreateHeaderElement(2);
                heading3.SetText("Section 1.2: Objectives");
                root.AppendChild(heading3);

                // Save the modified PDF (no PreSave needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with headings saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}