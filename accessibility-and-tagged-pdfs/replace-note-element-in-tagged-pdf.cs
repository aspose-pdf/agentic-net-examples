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
        const string newNoteText = "Updated note content goes here.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: always wrap Document in a using block)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Find the first existing NoteElement in the structure tree (recursive search)
            NoteElement existingNote = null;
            var notes = tagged.RootElement.FindElements<NoteElement>(true);
            if (notes != null && notes.Count > 0)
                existingNote = notes[0];

            // Remove the existing note if it was found
            if (existingNote != null)
                existingNote.Remove(); // removes the element from the structure tree and the document

            // Create a new NoteElement via the factory method
            NoteElement newNote = tagged.CreateNoteElement();

            // Set the note's text content
            newNote.SetText(newNoteText);

            // Append the new note to the root element (or any other appropriate parent)
            tagged.RootElement.AppendChild(newNote); // AppendChild with one argument (bool defaults)

            // Save the modified PDF (rule: Document.Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Note replaced and saved to '{outputPath}'.");
    }
}