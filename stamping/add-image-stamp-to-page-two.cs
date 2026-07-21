using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Verify that page 2 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Create an image stamp from the file
            ImageStamp imgStamp = new ImageStamp(stampImg);
            imgStamp.Quality = 100;   // 100 % quality
            imgStamp.Opacity = 0.8;   // 80 % opacity

            // Add the stamp to page 2
            Aspose.Pdf.Page pageTwo = doc.Pages[2];
            pageTwo.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}