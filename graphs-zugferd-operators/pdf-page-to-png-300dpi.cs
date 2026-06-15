using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Create a sample PDF with one page
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment("Sample PDF for PNG conversion");
            page.Paragraphs.Add(fragment);
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and convert the first page to PNG at 300 DPI
        using (Document pdfDocument = new Document("input.pdf"))
        {
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);
            using (FileStream pngStream = new FileStream("page1.png", FileMode.Create))
            {
                pngDevice.Process(pdfDocument.Pages[1], pngStream);
            }
        }
    }
}
