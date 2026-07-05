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
        const string outputPath = "output.pdf";
        const string newNoteText = "This is the updated note content.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (tagged PDF API)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Find existing NoteElement(s) in the structure (recursive search)
            var existingNotes = root.FindElements<NoteElement>(true);
            foreach (var note in existingNotes)
            {
                // Remove each existing note from the structure tree
                note.Remove();
            }

            // Create a new NoteElement via the factory method
            NoteElement updatedNote = taggedContent.CreateNoteElement();

            // Set the note's text content
            updatedNote.SetText(newNoteText);

            // Append the new note to the root element
            root.AppendChild(updatedNote); // AppendChild with single argument (bool defaults)

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated note: {outputPath}");
    }
}