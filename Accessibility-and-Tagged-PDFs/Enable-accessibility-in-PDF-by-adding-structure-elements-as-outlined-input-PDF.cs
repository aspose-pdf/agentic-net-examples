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
        const string outputPath = "accessible_output.pdf";

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
                // Access the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title (write‑only setters)
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast required)
                StructureElement root = tagged.RootElement;

                // Create a paragraph element and assign its text
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been made accessible by adding a paragraph structure element.");

                // Attach the paragraph to the root using AppendChild (single argument)
                root.AppendChild(paragraph);

                // Save the modified PDF (no PreSave call needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}