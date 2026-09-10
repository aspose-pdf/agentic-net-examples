using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "notes.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Access tagged content (if any)
                ITaggedContent tagged = doc.TaggedContent;

                // Get the root of the structure tree
                StructureElement root = tagged.RootElement;

                // Find all NoteElement objects recursively
                var notes = root.FindElements<NoteElement>(true);

                // Concatenate the ActualText of each note
                string allNotes = string.Empty;
                foreach (NoteElement note in notes)
                {
                    // Use ActualText (or AlternativeText) as the note's content
                    string text = note.ActualText ?? string.Empty;
                    if (!string.IsNullOrEmpty(text))
                    {
                        allNotes += text + Environment.NewLine;
                    }
                }

                // Write the concatenated notes to a plain text file
                File.WriteAllText(outputTxtPath, allNotes);
                Console.WriteLine($"Extracted notes saved to '{outputTxtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}