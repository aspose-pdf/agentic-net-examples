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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Access tagged content (structure tree)
            ITaggedContent taggedContent = doc.TaggedContent;
            StructureElement root = taggedContent.RootElement;

            // Find all Note elements recursively
            var noteElements = root.FindElements<NoteElement>(true);

            // Concatenate the actual text of each note
            StringBuilder sb = new StringBuilder();
            foreach (NoteElement note in noteElements)
            {
                if (!string.IsNullOrEmpty(note.ActualText))
                {
                    sb.AppendLine(note.ActualText);
                }
            }

            // Write the concatenated notes to a plain text file
            File.WriteAllText(outputTxtPath, sb.ToString());
        }

        Console.WriteLine($"Extracted notes saved to '{outputTxtPath}'.");
    }
}