using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string emfPath = "input.emf";
        const string pdfPath = "output.pdf";

        if (!File.Exists(emfPath))
        {
            Console.Error.WriteLine($"EMF file not found: {emfPath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Load the EMF image via a stream and add it to the page
            using (FileStream imgStream = new FileStream(emfPath, FileMode.Open, FileAccess.Read))
            {
                Image img = new Image
                {
                    ImageStream = imgStream
                };
                page.Paragraphs.Add(img);
            }

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EMF image successfully converted to PDF: {pdfPath}");
    }
}