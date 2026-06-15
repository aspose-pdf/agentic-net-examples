using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file
            Document doc = new Document();
            doc.Pages.Add();
            doc.Save("input.pdf");

            // Open the PDF and add a text stamp with auto‑adjusted font size
            using (Document pdfDoc = new Document("input.pdf"))
            {
                TextStamp stamp = new TextStamp("Sample Stamp");
                stamp.Width = 200;
                stamp.Height = 50;
                stamp.AutoAdjustFontSizeToFitStampRectangle = true;
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Center;

                // Apply the stamp to the first page (page indexing is 1‑based)
                pdfDoc.Pages[1].AddStamp(stamp);

                pdfDoc.Save("output.pdf");
            }
        }
    }
}