using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string htmlFile = "gallery.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        // Extract images and build HTML gallery
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\"><title>Image Gallery</title></head>");
            htmlBuilder.AppendLine("<body>");

            while (extractor.HasNextImage())
            {
                string imageFileName = "image-" + imageIndex + ".png";
                extractor.GetNextImage(imageFileName);

                htmlBuilder.AppendLine("<div style=\"display:inline-block;margin:5px;\">");
                htmlBuilder.AppendLine("<img src=\"" + imageFileName + "\" alt=\"Image " + imageIndex + "\" style=\"max-width:200px;\"/>");
                htmlBuilder.AppendLine("</div>");
                imageIndex++;
            }

            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            File.WriteAllText(htmlFile, htmlBuilder.ToString());
        }

        Console.WriteLine("Images extracted and HTML gallery created: " + htmlFile);
    }
}
