using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF file
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Sample PDF for image stamp.");
            page.Paragraphs.Add(tf);
            doc.Save("input.pdf");
        }

        // 1x1 pixel PNG (transparent) encoded in Base64
        string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/5+BAQAE/wJ/6V6ZAAAAAElFTkSuQmCC";
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Open the PDF and add a low‑quality image stamp (10%)
        using (Document pdfDoc = new Document("input.pdf"))
        {
            using (MemoryStream imgStream = new MemoryStream(imageBytes))
            {
                ImageStamp imgStamp = new ImageStamp(imgStream);
                imgStamp.Quality = 10; // 10 percent quality to reduce size
                imgStamp.Background = false;
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment = VerticalAlignment.Center;

                // Apply the stamp to each page (only one page in this example)
                foreach (Page pg in pdfDoc.Pages)
                {
                    pg.AddStamp(imgStamp);
                }
            }
            pdfDoc.Save("output.pdf");
        }
    }
}