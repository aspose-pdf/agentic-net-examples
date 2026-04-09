// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
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
        const string outputPath = "output.pdf";
        const string newNoteText = "This is the updated note content.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element
            StructureElement root = taggedContent.RootElement;

            // Find the first existing NoteElement in the structure tree (if any)
            NoteElement existingNote = null;
            foreach (var note in root.FindElements<NoteElement>(true))
            {
                existingNote = note;
                break; // only need the first one
            }

            // Remove the existing note element from the structure (if it exists)
            existingNote?.Remove();

            // Create a new NoteElement, set its text, and attach it to the root
            NoteElement newNote = taggedContent.CreateNoteElement();
            newNote.SetText(newNoteText);
            root.AppendChild(newNote);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}

// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This dummy source file satisfies the project reference that expects
// a file named "AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig".
// It contains valid C# code (an empty namespace) so the compiler can
// compile it without errors.
namespace Dummy { }
