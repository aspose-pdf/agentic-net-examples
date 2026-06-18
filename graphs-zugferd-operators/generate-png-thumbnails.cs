using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

namespace GeneratePngThumbnails
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with two pages
            string samplePath = "sample.pdf";
            using (Document sampleDoc = new Document())
            {
                // First page with text
                Page page1 = sampleDoc.Pages.Add();
                TextFragment fragment1 = new TextFragment("First page");
                page1.Paragraphs.Add(fragment1);

                // Second page with text
                Page page2 = sampleDoc.Pages.Add();
                TextFragment fragment2 = new TextFragment("Second page");
                page2.Paragraphs.Add(fragment2);

                sampleDoc.Save(samplePath);
            }

            // Open the sample PDF and generate thumbnails
            using (Document pdfDoc = new Document(samplePath))
            {
                // Create a ThumbnailDevice with custom size (e.g., 150x150 pixels)
                ThumbnailDevice thumbDevice = new ThumbnailDevice(150, 150);

                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string thumbPath = $"thumb_page{pageNumber}.png";
                    using (FileStream outputStream = new FileStream(thumbPath, FileMode.Create))
                    {
                        thumbDevice.Process(pdfDoc.Pages[pageNumber], outputStream);
                    }
                }
            }
        }
    }
}