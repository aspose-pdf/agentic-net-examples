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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create a Figure element and add it to the root
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample figure description";
            root.AppendChild(figure);

            // Create a Note element (caption) and attach it under the Figure
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This is a descriptive caption for the figure.");
            figure.AppendChild(note);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved with note under figure to '{outputPath}'.");
    }
}