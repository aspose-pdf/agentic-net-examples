using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a sample image file
        string imagePath = "sample.jpg";
        CreateSampleImage(imagePath);

        // Create a sample PDF document
        string pdfPath = "input.pdf";
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(pdfPath);
        }

        // Add the image to the PDF using PdfFileMend
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(pdfPath);
            // Add image to page 1 at position (100, 500) with size 200x200
            mend.AddImage(imagePath, 1, 100f, 500f, 200f, 200f);

            // Save the modified PDF into a memory stream
            using (MemoryStream outputStream = new MemoryStream())
            {
                mend.Save(outputStream);
                outputStream.Position = 0;

                // Write the PDF bytes to a file (console‑app friendly alternative to HTTP response)
                string outputPdfPath = "output.pdf";
                using (FileStream fileStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'. Size: {new FileInfo(outputPdfPath).Length} bytes.");
            }
        }
    }

    static void CreateSampleImage(string path)
    {
        using (Bitmap bmp = new Bitmap(100, 100))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.LightBlue);
                g.DrawEllipse(System.Drawing.Pens.Red, 10, 10, 80, 80);
            }
            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
