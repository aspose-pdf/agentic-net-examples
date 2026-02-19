using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document
        Document pdfDocument = new Document();

        // Add a blank page to the document
        Page page = pdfDocument.Pages.Add();

        // Create a text fragment with the desired content
        TextFragment text = new TextFragment("Hello, Aspose.Pdf!");

        // Set basic text formatting
        text.TextState.FontSize = 12;
        text.TextState.Font = FontRepository.FindFont("Arial");
        text.TextState.ForegroundColor = Color.Black;

        // Add the text fragment to the page
        page.Paragraphs.Add(text);

        // Save the document as a PDF file
        pdfDocument.Save("output.pdf");
    }
}