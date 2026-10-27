using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagesDir = "extracted_images";
        const string videoPath = "slideshow.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output folder for images exists
        Directory.CreateDirectory(imagesDir);

        // Extract all images from the PDF using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage(); // extracts images from the bound PDF

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as a sequentially numbered JPEG
                string imageFile = Path.Combine(imagesDir, $"image_{imageIndex:D3}.jpg");
                extractor.GetNextImage(imageFile);
                imageIndex++;
            }
        }

        // Verify that at least one image was extracted
        string[] imageFiles = Directory.GetFiles(imagesDir, "image_*.jpg");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No images were extracted from the PDF.");
            return;
        }

        // Build FFmpeg arguments to create a slideshow (1 second per image)
        string ffmpegArgs = $"-y -framerate 1 -i \"{Path.Combine(imagesDir, "image_%03d.jpg")}\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{videoPath}\"";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = ffmpegArgs,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        try
        {
            using (Process proc = Process.Start(psi))
            {
                string stdOut = proc.StandardOutput.ReadToEnd();
                string stdErr = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (proc.ExitCode == 0)
                {
                    Console.WriteLine($"Video slideshow created: {videoPath}");
                }
                else
                {
                    Console.Error.WriteLine("FFmpeg failed:");
                    Console.Error.WriteLine(stdErr);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error running ffmpeg: {ex.Message}");
        }
    }
}