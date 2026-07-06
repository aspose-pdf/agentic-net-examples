using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text is left‑aligned.");

            // Set the horizontal alignment of the text to Left
            fragment.TextState.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document (PDF format)
            doc.Save("AlignedLeft.pdf");
        }

        Console.WriteLine("PDF created with left‑aligned text.");
    }
}