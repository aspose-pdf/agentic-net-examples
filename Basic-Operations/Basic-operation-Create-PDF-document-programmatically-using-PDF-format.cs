using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        Document pdfDocument = new Document();

        // Add a blank page to the document
        Page page = pdfDocument.Pages.Add();

        // Add a simple text fragment to the page
        TextFragment text = new TextFragment("Hello, Aspose.Pdf!");
        page.Paragraphs.Add(text);

        // Save the document to a file (uses the provided document-save rule)
        pdfDocument.Save("output.pdf");
    }
}