using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputBmp = "page3.bmp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfConverter (required step)
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(doc);

            // Ensure form fields are rendered in Production mode
            converter.FormPresentationMode = FormPresentationMode.Production;

            // Convert page 3 to BMP image.
            // Use the constructor that accepts a Resolution – the Resolution property is read‑only.
            BmpDevice bmpDevice = new BmpDevice(new Resolution(300));

            // Process the specific page (pages are 1‑based)
            bmpDevice.Process(doc.Pages[3], outputBmp);
        }

        Console.WriteLine($"Page 3 saved as BMP to '{outputBmp}'.");
    }
}
