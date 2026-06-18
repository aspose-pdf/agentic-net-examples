using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string imagesFolder = "ExtractedImages";
        const string outputVideo = "slideshow.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        // Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 1;
            extractor.EndPage = int.MaxValue; // Process all pages
            extractor.Resolution = 150;       // Optional: set image resolution
            extractor.ExtractImage();         // Enable image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesFolder, $"img_{imageIndex:D4}.jpg");
                extractor.GetNextImage(imagePath); // Saves the next image as JPEG
                imageIndex++;
            }
        }

        // Verify that images were extracted
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.jpg");
        if (imageFiles.Length == 0)
        {
            Console.Error.WriteLine("No images were extracted from the PDF.");
            return;
        }

        // Build and run FFmpeg command to create a video slideshow from the extracted images
        // Assumes ffmpeg is available in the system PATH. Adjust ffmpegPath if needed.
        const string ffmpegPath = "ffmpeg";
        string inputPattern = Path.Combine(imagesFolder, "img_%04d.jpg");
        var psi = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = $"-y -framerate 1 -i \"{inputPattern}\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{outputVideo}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using (Process proc = Process.Start(psi))
            {
                // Capture any output for debugging purposes
                string stdOut = proc.StandardOutput.ReadToEnd();
                string stdErr = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (proc.ExitCode != 0)
                {
                    Console.Error.WriteLine($"FFmpeg exited with code {proc.ExitCode}.");
                    Console.Error.WriteLine(stdErr);
                }
                else
                {
                    Console.WriteLine($"Video slideshow created successfully: {outputVideo}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while running FFmpeg: {ex.Message}");
        }
    }
}
