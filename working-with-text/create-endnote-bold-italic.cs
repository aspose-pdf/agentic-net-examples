using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "endnote_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text fragment that will contain the endnote reference
            TextFragment tf = new TextFragment("Reference to endnote");

            // Create a Note object for the endnote
            Note endNote = new Note();
            endNote.Text = "This is an endnote with bold and italic styling.";

            // Apply bold and italic styles via TextState
            // TextState(string fontFamily, bool bold, bool italic) constructor sets the style
            endNote.TextState = new TextState("Helvetica", true, true);

            // Assign the endnote to the text fragment
            tf.EndNote = endNote;

            // Optionally set the position of the fragment on the page
            tf.Position = new Position(100, 700);

            // Add the fragment to the page
            page.Paragraphs.Add(tf);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with endnote saved to '{outputPath}'.");
    }
}