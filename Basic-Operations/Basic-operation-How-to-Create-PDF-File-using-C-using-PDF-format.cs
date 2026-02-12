using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        Document pdfDocument = new Document();

        // Add a page to the document
        Page page = pdfDocument.Pages.Add();

        // Add a simple text fragment to the page
        TextFragment text = new TextFragment("Hello, Aspose.Pdf!");
        page.Paragraphs.Add(text);

        // Save the PDF file (uses the document-save rule)
        string outputPath = "output.pdf";
        pdfDocument.Save(outputPath);
    }
}