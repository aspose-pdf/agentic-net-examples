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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content and set basic properties
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a figure element (e.g., representing an image)
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Diagram showing process flow.";
            root.AppendChild(figure); // Attach figure to the root

            // Create a note element to serve as a caption for the figure
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: Process flow diagram illustrating the main steps.");
            // Append the note under the figure element
            figure.AppendChild(note);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with figure and note saved to '{outputPath}'.");
    }
}