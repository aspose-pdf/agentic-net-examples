using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page to the document (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document to a file named NewDocument.pdf
            doc.Save("NewDocument.pdf");
        }

        Console.WriteLine("PDF created and saved as NewDocument.pdf");
    }
}