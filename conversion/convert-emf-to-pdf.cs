using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string emfPath = "image.emf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(emfPath))
        {
            Console.Error.WriteLine($"EMF file not found: {emfPath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create an Image object and assign the EMF file path
            Image img = new Image
            {
                File = emfPath
            };

            // Insert the image into the page's content
            page.Paragraphs.Add(img);

            // Save the document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"EMF converted to PDF: {outputPdf}");
    }
}