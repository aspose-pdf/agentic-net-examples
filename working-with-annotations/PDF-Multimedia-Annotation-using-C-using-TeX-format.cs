using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Resolve a data directory relative to the executable location.
        // This directory will be created automatically if it does not exist.
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        Directory.CreateDirectory(dataDir);

        // Paths for the generated PDF, the TeX export, and the media file to embed.
        string pdfPath = Path.Combine(dataDir, "MultimediaResult.pdf");
        string texPath = Path.Combine(dataDir, "Multimedia.tex");
        string mediaPath = Path.Combine(dataDir, "sample.mp4"); // video or audio file

        // Verify that the media file exists before attempting to embed it.
        if (!File.Exists(mediaPath))
        {
            Console.WriteLine($"Media file not found: {mediaPath}");
            Console.WriteLine("Please copy a valid MP4 file to the above location and re‑run the program.");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page to the document.
            Page page = doc.Pages.Add();

            // Define the rectangle where the RichMedia annotation will appear.
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation on the specified page and rectangle.
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Optional visual appearance settings.
                Color = Aspose.Pdf.Color.LightGray,
                Contents = "Embedded video example"
            };

            // Load the media file and attach it to the annotation.
            using (FileStream mediaStream = File.OpenRead(mediaPath))
            {
                // MIME type for MP4 video; adjust if using a different format.
                richMedia.SetContent("video/mp4", mediaStream);
            }

            // Optional: set a poster image (thumbnail) for the annotation.
            // Uncomment and provide a valid image file if required.
            // string posterPath = Path.Combine(dataDir, "poster.jpg");
            // if (File.Exists(posterPath))
            // {
            //     using (FileStream posterStream = File.OpenRead(posterPath))
            //     {
            //         richMedia.SetPoster(posterStream);
            //     }
            // }

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(richMedia);

            // Save the PDF so you can view the result if needed.
            doc.Save(pdfPath);

            // Export the PDF to TeX format using TeXSaveOptions.
            TeXSaveOptions texOptions = new TeXSaveOptions();
            doc.Save(texPath, texOptions);
        }

        Console.WriteLine("PDF with multimedia annotation created and exported to TeX.");
    }
}
