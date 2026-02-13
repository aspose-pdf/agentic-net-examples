using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document
        Document pdfDocument = new Document();

        // Add a blank page (optional, ensures the PDF has at least one page)
        pdfDocument.Pages.Add();

        // Save the document in PDF format
        pdfDocument.Save("output.pdf");
    }
}