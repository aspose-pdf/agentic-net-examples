using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_with_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the document (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Figure element that will represent the figure
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Illustration of the data analysis results.";
            // Append the figure to the root of the structure tree
            root.AppendChild(figure);

            // Create a Note element that will serve as the caption/description
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: Summary of quarterly revenue trends.");
            // Append the note as a child of the figure element
            figure.AppendChild(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with note saved to '{outputPath}'.");
    }
}