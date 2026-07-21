using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent interface
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language and title (metadata for accessibility)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Paragraph Title");

            // Get the root structure element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a new paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph provides a concise summary of the document content.");

            // Set the Title property on the paragraph element – this is the concise summary
            paragraph.Title = "Summary Paragraph";

            // Attach the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}