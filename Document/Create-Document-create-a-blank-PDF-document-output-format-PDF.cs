using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new blank PDF document
        Document pdfDocument = new Document();

        // Output file path
        string outputPath = "blank.pdf";

        // Save the document (simple save without options)
        pdfDocument.Save(outputPath);
    }
}