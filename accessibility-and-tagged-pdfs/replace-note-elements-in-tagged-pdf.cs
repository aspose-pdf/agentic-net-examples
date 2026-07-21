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
        const string outputPath = "updated_note.pdf";
        const string newNoteText = "This is the updated note content.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Find all existing NoteElement objects in the document
            var existingNotes = root.FindElements<NoteElement>(true);
            foreach (var note in existingNotes)
            {
                // Remove each note from the structure tree
                note.Remove();
            }

            // Create a new NoteElement via the factory method
            NoteElement updatedNote = tagged.CreateNoteElement();

            // Set the visible text of the note
            updatedNote.SetText(newNoteText);

            // Append the new note to the root of the structure tree
            root.AppendChild(updatedNote);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}