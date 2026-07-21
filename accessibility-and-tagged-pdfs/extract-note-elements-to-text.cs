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
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "notes.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Access tagged content (may be empty if the PDF is not tagged)
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Find all NoteElement instances recursively
            var notes = root.FindElements<NoteElement>(true);

            // Concatenate the ActualText of each note
            StringBuilder sb = new StringBuilder();
            foreach (NoteElement note in notes)
            {
                if (!string.IsNullOrEmpty(note.ActualText))
                {
                    sb.AppendLine(note.ActualText);
                }
            }

            // Write the concatenated text to a plain text file
            File.WriteAllText(outputTxtPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"All note texts have been saved to '{outputTxtPath}'.");
    }
}