using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;                 // ITaggedContent
using Aspose.Pdf.LogicalStructure;      // StructureElement, ParagraphElement, NoteElement

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

        // Load the PDF (no special load options needed for a plain PDF)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged document (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element that will contain the main text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is the main paragraph text.");

            // Create a note element (footnote/endnote) and set its content
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Supplemental information provided as a note.");

            // Attach the note as a child of the paragraph
            // AppendChild with a single argument uses the default bool (true)
            paragraph.AppendChild(note);

            // Attach the paragraph (with its note) to the document root
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with note: '{outputPath}'");
    }
}