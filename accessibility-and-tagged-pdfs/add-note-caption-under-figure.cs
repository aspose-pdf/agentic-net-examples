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
            // Optional metadata for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Figure and Note");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Figure element and attach it to the root
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample figure description";
            root.AppendChild(figure);

            // Create a Note element (caption) and attach it under the Figure
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This figure illustrates the sample data.");
            note.AlternativeText = "Caption for figure 1";
            figure.AppendChild(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}