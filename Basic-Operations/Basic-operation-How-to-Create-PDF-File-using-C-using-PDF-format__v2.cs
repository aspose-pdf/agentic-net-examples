using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "created.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            Page page = pdfDoc.Pages.Add();

            // Create a text fragment with sample content
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document in PDF format (default)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}