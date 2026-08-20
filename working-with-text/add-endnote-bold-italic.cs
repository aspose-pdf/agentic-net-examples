using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_endnote.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF, modify it, and save.
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment that will contain the endnote.
            TextFragment fragment = new TextFragment("Sample paragraph with an endnote.");

            // Create the endnote content.
            Note endNote = new Note("This is the endnote text.");

            // Define bold and italic style via TextState.
            // Constructor (fontFamily, bold, italic) sets both styles.
            TextState style = new TextState("Helvetica", true, true);
            endNote.TextState = style;

            // Attach the endnote to the text fragment.
            fragment.EndNote = endNote;

            // Add the fragment to the first page.
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with endnote: {outputPath}");
    }
}