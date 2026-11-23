using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_tagged.pdf";
        const string outputPath = "output_with_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the tagged PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document is tagged (Tagging can be added even if it wasn't)
            // Set language and title (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a new table element and attach it to the root
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample data table with footer note";
            root.AppendChild(table);

            // Create the table footer (TFoot) element
            TableTFootElement tfoot = taggedContent.CreateTableTFootElement();
            table.AppendChild(tfoot);

            // Create a Note element, set its text, and add it to the footer
            NoteElement note = taggedContent.CreateNoteElement();
            note.SetText("This is a footnote placed in the table footer.");
            tfoot.AppendChild(note);

            // Save the modified PDF (default PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with note element: {outputPath}");
    }
}