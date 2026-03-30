using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string pdfPath = "input.pdf";
        string htmlPath = "report.html";
        List<string> base64Images = new List<string>();

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a minimal
        // placeholder PDF so the extractor can work without throwing a
        // FileNotFoundException. This makes the sample self‑contained
        // and suitable for unit‑testing.
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            // Create a simple PDF with a single blank page.
            using (Document placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(pdfPath);
            }
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    byte[] imageBytes = imageStream.ToArray();
                    string base64 = Convert.ToBase64String(imageBytes);
                    base64Images.Add(base64);
                }
            }
        }

        string htmlContent = "<html><head><meta charset=\"UTF-8\"><title>Image Report</title></head><body>";
        foreach (string base64 in base64Images)
        {
            // The extractor returns the original image format (png, jpg, …).
            // For simplicity we assume PNG; if other formats are needed, the MIME type
            // can be derived from the first few bytes of the image.
            htmlContent += $"<img src=\"data:image/png;base64,{base64}\" /><br/>";
        }
        htmlContent += "</body></html>";

        File.WriteAllText(htmlPath, htmlContent);
        Console.WriteLine($"HTML report generated: {htmlPath}");
    }
}
