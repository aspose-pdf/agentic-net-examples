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
        const string outputPath = "output_with_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Note");

            StructureElement root = tagged.RootElement;

            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is the main paragraph.");
            root.AppendChild(paragraph);

            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Supplemental note providing extra information.");
            paragraph.AppendChild(note);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with note to '{outputPath}'.");
    }
}