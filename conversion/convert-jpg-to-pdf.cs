using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jpgPath = "input.jpg";
        const string pdfPath = "output.pdf";

        if (!File.Exists(jpgPath))
        {
            Console.Error.WriteLine($"Image file not found: {jpgPath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create an Aspose.Pdf.Image and set its source file
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                File = jpgPath
            };

            // Add the image to the page's paragraphs collection
            page.Paragraphs.Add(img);

            // Save the document as PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"JPG image converted to PDF: {pdfPath}");
    }
}