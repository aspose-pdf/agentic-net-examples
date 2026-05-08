using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF containing notes
        const string outputTxt = "notes.txt";      // destination plain‑text file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (required for logical structure elements)
            ITaggedContent tagged = doc.TaggedContent;

            // Find all NoteElement instances in the structure tree (recursive search)
            var noteElements = tagged.RootElement.FindElements<NoteElement>(true);

            // Concatenate the actual text of each note
            StringBuilder sb = new StringBuilder();
            foreach (NoteElement note in noteElements)
            {
                // ActualText holds the note's visible text
                sb.AppendLine(note.ActualText ?? string.Empty);
            }

            // Write the concatenated notes to a plain‑text file
            File.WriteAllText(outputTxt, sb.ToString());
        }

        Console.WriteLine($"All notes extracted to '{outputTxt}'.");
    }
}