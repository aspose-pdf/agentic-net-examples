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
            // Obtain the tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Figure element (represents an illustration such as an image)
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Illustration of the main concept";

            // Attach the figure to the root element
            root.AppendChild(figure); // AppendChild with a single argument

            // Create a Note element that will act as the caption/description
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This diagram illustrates the workflow of the system.");
            note.AlternativeText = "Caption for Figure 1";

            // Append the note under the figure element
            figure.AppendChild(note); // Note becomes a child of the figure

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with caption saved to '{outputPath}'.");
    }
}
