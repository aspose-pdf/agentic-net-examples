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

        // Video files – one per page (adjust the array to match your document).
        string[] videoFiles = new string[]
        {
            "video1.mp4",
            "video2.mp4",
            "video3.mp4"
            // Add more entries if the PDF has more pages.
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            if (videoFiles.Length < pageCount)
            {
                Console.Error.WriteLine("Insufficient video files for the number of pages.");
                return;
            }

            // Add a ScreenAnnotation to each page.
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];

                // Define the annotation rectangle (example size and position).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 300);

                // Create the screen annotation with the corresponding video file.
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoFiles[i - 1]);

                // Optional: set title and contents for the annotation.
                screen.Title = $"Video {i}";
                screen.Contents = $"Play {Path.GetFileName(videoFiles[i - 1])}";

                // Add the annotation to the page.
                page.Annotations.Add(screen);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotations added. Output saved to '{outputPdf}'.");
    }
}