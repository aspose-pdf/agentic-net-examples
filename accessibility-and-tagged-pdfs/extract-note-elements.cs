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
        const string outputPath = "notes.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            var notes = root.FindElements<NoteElement>(true);
            string concatenated = string.Empty;
            foreach (var note in notes)
            {
                if (!string.IsNullOrEmpty(note.ActualText))
                {
                    concatenated += note.ActualText;
                }
            }

            File.WriteAllText(outputPath, concatenated);
        }

        Console.WriteLine($"Notes extracted to '{outputPath}'.");
    }
}