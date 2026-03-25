using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            // Optional: set language and title for the tagged content
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Note");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is a paragraph with a supplemental note.");

            // Create a note element (footnote/endnote)
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("This is the note providing additional information.");

            // Attach the note as a child of the paragraph
            paragraph.AppendChild(note);

            // Add the paragraph (with its note) to the document structure
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with note to '{outputPath}'.");
    }
}