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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API (creates a structure tree if missing)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a figure element (e.g., for an image) and set its alt text
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Alt text describing the figure content.";
            root.AppendChild(figure); // Attach figure to the root

            // Create a note element to serve as a caption under the figure
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This is a custom caption providing a descriptive explanation of the figure.");
            // Append the note as a child of the figure element
            figure.AppendChild(note);

            // Persist the changes
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with caption saved to '{outputPath}'.");
    }
}