using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input_tagged.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing tagged PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language and title for the document
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree
            var root = taggedContent.RootElement;

            // Create a new Section element and attach it to the root
            var section = taggedContent.CreateSectElement();
            root.AppendChild(section);

            // Create a Header (level 1) inside the section
            var header = taggedContent.CreateHeaderElement(1);
            header.SetText("New Section Header");
            section.AppendChild(header);

            // Create a Note element and embed it within the header
            var note = taggedContent.CreateNoteElement();
            note.SetText("This note is attached to the header.");
            header.AppendChild(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
