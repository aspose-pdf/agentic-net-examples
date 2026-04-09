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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find all NoteElement instances in the structure tree (recursive search)
            var noteElements = taggedContent.RootElement.FindElements<NoteElement>(true);

            // Concatenate the ActualText of each note element
            string concatenatedNotes = string.Empty;
            foreach (var note in noteElements)
            {
                // Use ActualText property which holds the note's textual content
                if (!string.IsNullOrEmpty(note.ActualText))
                {
                    concatenatedNotes += note.ActualText + Environment.NewLine;
                }
            }

            // Save the concatenated text to a plain .txt file
            File.WriteAllText(outputTxtPath, concatenatedNotes);
        }

        Console.WriteLine($"All note texts have been extracted to '{outputTxtPath}'.");
    }
}