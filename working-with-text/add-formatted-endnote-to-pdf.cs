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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a new text fragment that will hold the endnote reference
            TextFragment tf = new TextFragment("Reference");
            // Add the fragment to the first page (adjust page index as needed)
            doc.Pages[1].Paragraphs.Add(tf);

            // Create the endnote (Note object) and set its text
            Note endNote = new Note();
            endNote.Text = "This is a formatted endnote with bold and italic style.";

            // Create a TextState with bold and italic flags
            // Constructor: TextState(string fontFamily, bool bold, bool italic)
            TextState ts = new TextState("Helvetica", true, true);
            // Optionally set font size and color
            ts.FontSize = 12;
            ts.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the TextState to the endnote
            endNote.TextState = ts;

            // Assign the endnote to the TextFragment
            tf.EndNote = endNote;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with endnote to '{outputPath}'.");
    }
}