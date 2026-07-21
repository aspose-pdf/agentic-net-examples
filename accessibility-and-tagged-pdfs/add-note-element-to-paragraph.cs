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

        // Load the PDF and obtain the tagged‑content helper
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and set its main text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is the main paragraph text.");

            // Create a note element (footnote/endnote) and set its supplemental text
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Supplemental information provided as a note.");

            // Attach the note as a child of the paragraph
            paragraph.AppendChild(note);   // bool parameter omitted – default is true

            // Add the paragraph (with its note) to the document structure
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with note element: '{outputPath}'");
    }
}