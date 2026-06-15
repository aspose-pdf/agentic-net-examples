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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the whole document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a Figure element (represents an illustration such as an image)
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Illustration of the main concept.";
            root.AppendChild(figure); // attach figure to the root

            // Create a Note element to serve as a caption/description
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This diagram illustrates the workflow of the system.");
            // Alternatively you can set AlternativeText or ActualText as needed
            // note.AlternativeText = "Caption for Figure 1";

            // Append the note under the figure element
            figure.AppendChild(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with note caption saved to '{outputPath}'.");
    }
}