using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string imagesDir = "ExtractedImages";
        const string csvPath   = "manifest.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF using PdfFileMend as required.
        // PdfFileMend only supports AddImage / AddText; we just bind here.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPath);

        // Ensure the folder for extracted images exists.
        Directory.CreateDirectory(imagesDir);

        // List to hold CSV rows.
        List<string> csvLines = new List<string>();
        csvLines.Add("ImageName,PageNumber");

        // Open the document to enumerate pages and images.
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages (1‑based indexing).
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageIndex = 1;

                // Iterate images on the current page.
                foreach (XImage img in page.Resources.Images)
                {
                    // Default to PNG extension.
                    string extension = ".png";

                    // Build a deterministic file name.
                    string imageName = $"Page{pageNum}_Image{imageIndex}{extension}";
                    string imagePath = Path.Combine(imagesDir, imageName);

                    // Save the image to disk using a FileStream (XImage.Save expects a Stream).
                    using (FileStream fs = File.Create(imagePath))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }

                    // Record the image name and its page number in the CSV.
                    csvLines.Add($"{imageName},{pageNum}");

                    imageIndex++;
                }
            }
        }

        // Write the CSV manifest.
        File.WriteAllLines(csvPath, csvLines, Encoding.UTF8);

        // Unbind is not required/available for PdfFileMend; the object will be disposed when out of scope.
        // mend.UnbindPdf(); // Removed because the method does not exist.

        Console.WriteLine($"Images extracted to '{imagesDir}'.");
        Console.WriteLine($"CSV manifest saved to '{csvPath}'.");
    }
}
