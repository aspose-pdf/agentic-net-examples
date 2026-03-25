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

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Locate existing Note elements in the structure tree
            var existingNotes = tagged.RootElement.FindElements<NoteElement>(true);
            if (existingNotes.Count > 0)
            {
                // Remove the first found note element
                existingNotes[0].Remove();
            }

            // Create a new Note element with the updated text
            NoteElement newNote = tagged.CreateNoteElement();
            newNote.SetText(newNoteText);

            // Append the new note to the root of the structure tree
            tagged.RootElement.AppendChild(newNote);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}