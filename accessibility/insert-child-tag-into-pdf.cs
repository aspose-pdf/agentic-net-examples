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

        // Load the PDF; existing tags are preserved automatically.
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface.
            ITaggedContent tagged = doc.TaggedContent;

            // Choose a parent element. Use the root element by default.
            StructureElement parent = tagged.RootElement;

            // Optionally, locate a specific existing element (e.g., first paragraph) to act as parent.
            var paragraphs = parent.FindElements<ParagraphElement>(true);
            if (paragraphs.Count > 0)
            {
                parent = paragraphs[0];
            }

            // Create a new paragraph element to insert.
            ParagraphElement newParagraph = tagged.CreateParagraphElement();
            newParagraph.SetText("Inserted accessible paragraph.");

            // Insert the new element as the first child of the chosen parent.
            // InsertChild(Element element, int index) – index 0 inserts at the beginning.
            parent.InsertChild(newParagraph, 0);

            // Save the modified PDF; all original tags remain intact.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with inserted tag to '{outputPath}'.");
    }
}