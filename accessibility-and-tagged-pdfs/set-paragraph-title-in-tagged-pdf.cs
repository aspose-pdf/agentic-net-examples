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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document-level title (not required for the paragraph)
            // tagged.SetTitle("Document with Summary Paragraph");

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a new paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the title of the paragraph – this provides a concise summary
            paragraph.Title = "Executive Summary";

            // Set the visible text of the paragraph (optional)
            paragraph.SetText("This paragraph gives a brief overview of the document's contents.");

            // Append the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Paragraph title set and PDF saved to '{outputPath}'.");
    }
}