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
        const string outputPath = "output_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optionally set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and set its main text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is a main paragraph.");

            // Create a note element to provide supplemental information
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Supplemental note providing additional information.");

            // Append the note as a child of the paragraph
            paragraph.AppendChild(note); // AppendChild with one argument

            // Append the paragraph (with its note) to the document root
            root.AppendChild(paragraph);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with note saved to '{outputPath}'.");
    }
}