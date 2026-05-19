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
        const string outputPath = "output.pdf";
        const string newNoteText = "This is the updated note.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent tagged = doc.TaggedContent;

                // Get the root structure element (no cast needed)
                StructureElement root = tagged.RootElement;

                // Locate any existing NoteElement(s) and remove them from the structure
                var existingNotes = root.FindElements<NoteElement>(true);
                foreach (var note in existingNotes)
                {
                    note.Remove(); // removes the element and its references
                }

                // Create a new NoteElement and set its text
                NoteElement newNote = tagged.CreateNoteElement();
                newNote.SetText(newNoteText);

                // Append the new note to the root of the structure tree
                root.AppendChild(newNote);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}