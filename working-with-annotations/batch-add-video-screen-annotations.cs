using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Video files – one per page (ensure the array length matches the PDF page count)
        string[] videoFiles = { "video1.mp4", "video2.mp4", "video3.mp4" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Optional: verify video files exist
        foreach (string vf in videoFiles)
        {
            if (!File.Exists(vf))
            {
                Console.Error.WriteLine($"Video file not found: {vf}");
                return;
            }
        }

        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            if (videoFiles.Length < pageCount)
            {
                Console.Error.WriteLine("Insufficient video files for the number of pages.");
                return;
            }

            // Iterate pages (1‑based indexing)
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];

                // Define annotation rectangle (example coordinates)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create ScreenAnnotation with the corresponding video file
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoFiles[i - 1]);

                // Optional metadata
                screen.Title = $"Video {i}";
                screen.Contents = $"Play {Path.GetFileName(videoFiles[i - 1])}";

                // Add annotation to the page
                page.Annotations.Add(screen);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotations added. Output saved to '{outputPdf}'.");
    }
}