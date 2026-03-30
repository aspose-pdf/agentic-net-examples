using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("File not found: " + inputPdf);
            return;
        }

        // Load the PDF to obtain the total page count.
        Document doc = new Document(inputPdf);
        int pageCount = doc.Pages.Count;

        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the same PDF file.
                extractor.BindPdf(inputPdf);

                // Restrict extraction to the current page.
                // PdfExtractor does not expose an ExtractPage method in recent versions;
                // instead, set the StartPage and EndPage properties.
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Extract images from the selected page.
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outputFileName = $"Image_Page{pageNumber}_Index{imageIndex}.png";
                    extractor.GetNextImage(outputFileName);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
