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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Figure element (e.g., to represent an image)
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample figure description";

            // Bind the figure to the first image on the first page (if any)
            if (doc.Pages.Count > 0)
            {
                Page page = doc.Pages[1];
                foreach (XImage img in page.Resources.Images)
                {
                    figure.Tag(img); // associate the figure with the image resource
                    break; // bind only the first image
                }
            }

            // Attach the figure to the root of the structure tree
            root.AppendChild(figure);

            // Create a Note element to serve as a caption/description
            NoteElement note = tagged.CreateNoteElement();
            note.SetText("Figure 1: This chart illustrates quarterly revenue growth.");

            // Append the note under the figure element
            figure.AppendChild(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}