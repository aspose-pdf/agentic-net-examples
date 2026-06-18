using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

public class ConvertPdfToPng
{
    public static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            Page page1 = sampleDoc.Pages.Add();
            page1.Paragraphs.Add(new TextFragment("Sample page 1"));
            Page page2 = sampleDoc.Pages.Add();
            page2.Paragraphs.Add(new TextFragment("Sample page 2"));
            sampleDoc.Save("input.pdf");
        }

        // Load the PDF and convert each page to PNG at 300 DPI
        using (Document pdfDocument = new Document("input.pdf"))
        {
            // Create a Resolution object with 300 DPI
            Resolution resolution = new Resolution(300);
            // PngDevice does NOT implement IDisposable – instantiate without a using block
            PngDevice pngDevice = new PngDevice(resolution);

            // Loop through all pages (1‑based indexing as required by Aspose.Pdf)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"image{pageNumber}_out.png";
                using (FileStream pngStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }
    }
}
