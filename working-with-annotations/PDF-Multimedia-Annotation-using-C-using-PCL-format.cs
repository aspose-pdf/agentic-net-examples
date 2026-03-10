using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string videoPath = "sample.mp4";
        const string posterPath = "poster.jpg";
        const string pdfOutput = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }
        if (!File.Exists(posterPath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterPath}");
            return;
        }

        // Load the PCL document using PclLoadOptions (the class resides in Aspose.Pdf namespace)
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were created from the PCL file.");
                return;
            }

            Page page = doc.Pages[1];
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create RichMediaAnnotation (no Type property – the content type is inferred from the attached file)
            var richMedia = new RichMediaAnnotation(page, rect)
            {
                Name = "SampleVideo",
                Contents = "Click to play video"
            };

            // Attach video content
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Attach poster image (preview shown before activation)
            using (FileStream posterStream = File.OpenRead(posterPath))
            {
                richMedia.SetPoster(posterStream);
            }

            page.Annotations.Add(richMedia);
            doc.Save(pdfOutput);
        }

        Console.WriteLine($"Multimedia PDF created: {pdfOutput}");
    }
}