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
        const string newNoteText = "Updated note content";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Find any existing NoteElement(s) in the structure tree
            var existingNotes = root.FindElements<NoteElement>(true);
            if (existingNotes.Count > 0)
            {
                // Remove the first found note (you could iterate if multiple need removal)
                existingNotes[0].Remove();
            }

            // Create a new NoteElement, set its text, and attach it to the root
            NoteElement updatedNote = taggedContent.CreateNoteElement();
            updatedNote.SetText(newNoteText);
            root.AppendChild(updatedNote); // AppendChild with a single argument (bool defaults)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Note replaced and saved to '{outputPath}'.");
    }
}