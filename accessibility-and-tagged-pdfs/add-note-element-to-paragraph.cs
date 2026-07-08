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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and set its text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is a main paragraph that will contain a note.");

            // Append the paragraph to the root
            root.AppendChild(paragraph);

            // Create a note element (supplemental information)
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("This is the supplemental note providing additional context.");

            // Append the note as a child of the paragraph
            paragraph.AppendChild(note); // bool parameter omitted (default true)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with note element: {outputPath}");
    }
}