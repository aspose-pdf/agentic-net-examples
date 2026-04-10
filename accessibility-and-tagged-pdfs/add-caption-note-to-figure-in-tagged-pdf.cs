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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Figure element (represents an illustration such as an image)
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Illustration of the main concept";

            // Append the Figure to the document root
            root.AppendChild(figure);

            // Create a Note element to serve as a caption/description
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This diagram illustrates the workflow of the system.");

            // Append the Note under the Figure element
            figure.AppendChild(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}