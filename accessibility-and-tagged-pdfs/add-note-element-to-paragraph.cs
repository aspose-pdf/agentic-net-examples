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
        const string outputPath = "output_with_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and set its visible text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is the main paragraph content.");

            // Create a note element (footnote/endnote) and set its text
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Supplemental information provided as a note.");

            // Attach the note as a child of the paragraph
            paragraph.AppendChild(note); // bool parameter omitted (default)

            // Attach the paragraph (with its note) to the document root
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with note element: {outputPath}");
    }
}