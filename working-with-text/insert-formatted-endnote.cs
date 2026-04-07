using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a text fragment that will contain the endnote
            TextFragment fragment = new TextFragment("Sample paragraph with an endnote.");

            // Create the endnote (Note) and set its text
            Note endNote = new Note("This is the formatted endnote text.");

            // Apply bold and italic styles via TextState
            // TextState(string fontFamily, bool bold, bool italic) sets the desired styles
            endNote.TextState = new TextState("Helvetica", true, true);

            // Assign the endnote to the text fragment
            fragment.EndNote = endNote;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF to disk
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with formatted endnote saved as 'output.pdf'.");
    }
}