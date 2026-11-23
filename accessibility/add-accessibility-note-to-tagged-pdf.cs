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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a Note element via the ITaggedContent factory
            NoteElement note = tagged.CreateNoteElement();

            // Set the visible text of the note
            note.SetText("This is an accessibility note added programmatically.");

            // Optionally provide alternative (alt) text for assistive technologies
            note.AlternativeText = "Accessibility note";

            // Append the note to the root of the structure tree
            root.AppendChild(note);

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with NoteElement saved to '{outputPath}'.");
    }
}