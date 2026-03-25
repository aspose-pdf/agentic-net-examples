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
        const string newNoteText = "Updated note content.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                ITaggedContent tagged = doc.TaggedContent;
                StructureElement root = tagged.RootElement;

                // Locate and remove any existing Note elements
                var existingNotes = root.FindElements<NoteElement>(true);
                foreach (var note in existingNotes)
                {
                    note.Remove();
                }

                // Create a new Note element with updated text
                NoteElement newNote = tagged.CreateNoteElement();
                newNote.SetText(newNoteText);

                // Append the new Note to the document's root structure element
                root.AppendChild(newNote);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Note replaced and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}