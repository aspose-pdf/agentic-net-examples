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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the PDF (writes to the tagged metadata)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a new paragraph element and set its text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible by adding a paragraph element.");

            // Append the paragraph to the root of the structure tree
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Persist the changes to the tagged structure inside the PDF
            tagged.Save();

            // Finally save the whole document to the output file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}