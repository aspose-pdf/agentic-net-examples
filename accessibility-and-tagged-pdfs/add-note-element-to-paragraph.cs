using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement, NoteElement

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and add it to the root
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is the main paragraph text.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Create a note element (footnote/endnote) and set its text
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Supplemental information provided as a note.");

            // Attach the note as a child of the paragraph
            paragraph.AppendChild(note); // Note becomes a child of the paragraph

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with note saved to '{outputPath}'.");
    }
}