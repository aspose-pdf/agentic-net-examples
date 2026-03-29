using System;
using System.IO;
using Aspose.Pdf;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // List of PNG image files to be added to the PDF (modify paths as needed)
        string[] imageFiles = new string[] { "image1.png", "image2.png", "image3.png" };

        using (Document doc = new Document())
        {
            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"File not found: {imgPath}");
                    continue;
                }

                // Add a new page for each image
                Page page = doc.Pages.Add();

                // Create an Image object and set its source file
                Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                img.File = imgPath;

                // Add the image to the page's paragraph collection (default margins are used)
                page.Paragraphs.Add(img);
            }

            // Save the combined PDF document
            doc.Save("output.pdf");
        }
    }
}