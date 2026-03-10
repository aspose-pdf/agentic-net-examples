using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Define a working directory. Adjust this path as needed for your
        //    environment. The directory will be created automatically if it does
        //    not exist.
        // ---------------------------------------------------------------------
        string dataDir = Path.Combine(Environment.CurrentDirectory, "Data");
        Directory.CreateDirectory(dataDir); // Guarantees the folder exists

        // ---------------------------------------------------------------------
        // 2. Input multimedia file (video) – make sure the file is present.
        // ---------------------------------------------------------------------
        string videoPath = Path.Combine(dataDir, "sample.mp4");
        if (!File.Exists(videoPath))
        {
            Console.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // 3. Output XPS file – also placed inside the working directory.
        // ---------------------------------------------------------------------
        string xpsPath = Path.Combine(dataDir, "multimedia.xps");

        // ---------------------------------------------------------------------
        // 4. Create a new PDF document and add a Screen annotation that embeds
        //    the video file.
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Rectangle: left, bottom, right, top (points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the ScreenAnnotation first
            ScreenAnnotation mediaAnn = new ScreenAnnotation(page, rect, videoPath)
            {
                Color = Aspose.Pdf.Color.LightGray
            };

            // Border requires the parent annotation in its constructor
            mediaAnn.Border = new Border(mediaAnn) { Width = 1 };

            page.Annotations.Add(mediaAnn);

            // Save the document as XPS
            XpsSaveOptions xpsOpts = new XpsSaveOptions();
            doc.Save(xpsPath, xpsOpts);
        }

        Console.WriteLine("PDF with multimedia annotation saved as XPS at:");
        Console.WriteLine(xpsPath);
    }
}
