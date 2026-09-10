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
        const string outputPath = "output_with_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and obtain the tagged‑content interface
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged document (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a Figure element (represents an illustration such as an image)
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Illustration of the main concept";

            // Append the Figure to the root of the structure tree
            root.AppendChild(figure);

            // Create a Note element that will serve as the caption/description
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This diagram illustrates the workflow of the system.");
            // Optionally set alternative text for assistive technologies
            note.AlternativeText = "Caption for Figure 1";

            // Append the Note as a child of the Figure element
            figure.AppendChild(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with caption note: {outputPath}");
    }
}