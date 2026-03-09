using System;
using Aspose.Pdf; // Aspose.Pdf namespace contains the Document class

class Program
{
    static void Main()
    {
        // Create a new empty PDF document using the default constructor.
        // The using statement ensures the document is properly disposed.
        using (Document pdfDoc = new Document())
        {
            // At this point you can add pages, text, images, etc.
            // Example: add a blank page (optional)
            pdfDoc.Pages.Add();

            // Save the document if needed (optional)
            // pdfDoc.Save("output.pdf");
        }

        Console.WriteLine("PDF Document instance created and disposed successfully.");
    }
}