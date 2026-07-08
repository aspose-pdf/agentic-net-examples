using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "notes.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (structure tree)
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Find all NoteElement instances in the structure (recursive)
            var notes = root.FindElements<NoteElement>(true);

            // Concatenate the actual text of each note
            string concatenated = string.Empty;
            foreach (NoteElement note in notes)
            {
                // Use ActualText property which holds the note's content
                if (!string.IsNullOrEmpty(note.ActualText))
                {
                    concatenated += note.ActualText + Environment.NewLine;
                }
            }

            // Save the concatenated text to a plain text file
            File.WriteAllText(outputTxt, concatenated);
        }

        Console.WriteLine($"All notes extracted to '{outputTxt}'.");
    }
}