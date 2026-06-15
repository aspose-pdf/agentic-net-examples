using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF and attach an embedded file
        using (Document createDoc = new Document())
        {
            // Add a page with some text
            createDoc.Pages.Add();
            TextFragment fragment = new TextFragment("Sample PDF with embedded file.");
            createDoc.Pages[1].Paragraphs.Add(fragment);

            // Create a dummy file to embed
            string embeddedFilePath = "sample.txt";
            File.WriteAllText(embeddedFilePath, "This is an embedded file.");

            // Attach the file as an embedded file (use Name property, not FileName)
            FileSpecification fileSpec = new FileSpecification("sample.txt", embeddedFilePath);
            createDoc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF
            createDoc.Save("input.pdf");
        }

        // Create a tiny PNG image to use as a stamp (1x1 pixel transparent)
        string stampImagePath = "stamp.png";
        byte[] pngBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=");
        File.WriteAllBytes(stampImagePath, pngBytes);

        // Load the PDF, add the image stamp, and preserve embedded files
        using (Document pdfDoc = new Document("input.pdf"))
        {
            int pageCount = pdfDoc.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                ImageStamp stamp = new ImageStamp(stampImagePath);
                stamp.LeftMargin = 10;
                stamp.TopMargin = 10;
                stamp.Opacity = 0.5f;
                pdfDoc.Pages[i].AddStamp(stamp);
            }

            // Save the stamped PDF; embedded files remain intact
            pdfDoc.Save("output.pdf");
        }
    }
}