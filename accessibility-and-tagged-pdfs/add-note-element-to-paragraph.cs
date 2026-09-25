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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content (creates a tagged structure if none exists)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a paragraph element
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();
            paragraph.SetText("This is the main paragraph text.");

            // Create a note element as a child of the paragraph
            NoteElement note = taggedContent.CreateNoteElement();
            note.SetText("Supplemental information provided in the note.");

            // Attach the note to the paragraph
            paragraph.AppendChild(note); // one-argument AppendChild

            // Attach the paragraph (with its note) to the root
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with note element: '{outputPath}'");
    }
}