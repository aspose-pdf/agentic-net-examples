using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment that will contain the endnote
            TextFragment fragment = new TextFragment("This paragraph includes an endnote.");

            // Create the endnote with the desired text
            Note endNote = new Note("This is the formatted endnote.");

            // Apply bold and italic styles via TextState (Helvetica, bold, italic)
            endNote.TextState = new TextState("Helvetica", true, true);

            // Associate the endnote with the text fragment
            fragment.EndNote = endNote;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}