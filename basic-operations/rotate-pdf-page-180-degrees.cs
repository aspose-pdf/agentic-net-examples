using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a PDF in memory with at least three pages.
        Document doc = CreateSampleDocument();

        // Rotate page 3 by 180 degrees.
        doc.Pages[3].Rotate = Rotation.on180;

        // Save the modified PDF.
        doc.Save("output.pdf");
    }

    private static Document CreateSampleDocument()
    {
        // Initialise a new document.
        Document document = new Document();

        // Ensure the document has at least three pages.
        for (int i = 0; i < 3; i++)
        {
            document.Pages.Add();
        }

        return document;
    }
}