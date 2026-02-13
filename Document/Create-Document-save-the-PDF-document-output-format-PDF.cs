using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new, empty PDF document
        Document pdfDocument = new Document();

        // Save the document in PDF format
        pdfDocument.Save("output.pdf");
    }
}