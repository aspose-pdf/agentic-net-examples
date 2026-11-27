using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";
        const string reportPath = "image_report.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document doc = new Document(pdfPath))
        using (StreamWriter report = new StreamWriter(reportPath, false))
        {
            int imageCounter = 0;
            // Pages are 1‑based
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                // Iterate over XImage collection (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    imageCounter++;
                    string imageFileName = $"image_page{pageNum}_{imageCounter}.jpg";
                    string imagePath = Path.Combine(outputDir, imageFileName);
                    // Save the image; XImage.Save expects a Stream, not a file path.
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }
                    // Write mapping to report
                    report.WriteLine($"{imageFileName} -> Page {pageNum}");
                }
            }
        }

        Console.WriteLine("Image extraction completed. Report written to image_report.txt");
    }
}
