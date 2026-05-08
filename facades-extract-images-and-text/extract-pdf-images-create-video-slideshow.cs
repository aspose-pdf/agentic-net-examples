using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "input.pdf";
        const string imagesDir = "extracted_images";
        const string videoPath = "slideshow.mp4";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure output folder for images exists
        Directory.CreateDirectory(imagesDir);

        // ---------- Extract images from PDF ----------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a zero‑padded filename (image_001.png, image_002.png, …)
                string imagePath = Path.Combine(imagesDir, $"image_{imageIndex:D3}.png");

                // Extract the next image as PNG; GetNextImage returns true on success
                bool extracted = extractor.GetNextImage(imagePath, ImageFormat.Png);
                if (!extracted)
                {
                    Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                }

                imageIndex++;
            }
        }

        // Verify that at least one image was extracted before invoking FFmpeg
        if (!Directory.EnumerateFiles(imagesDir, "*.png").Any())
        {
            Console.Error.WriteLine("No images were extracted – aborting video creation.");
            return;
        }

        // ---------- Generate video slideshow with FFmpeg ----------
        // Assumes ffmpeg is available in the system PATH
        const string ffmpegExe = "ffmpeg";

        // -framerate 1  → 1 image per second (adjust as required)
        // -i image_%03d.png  → input pattern matching the extracted files
        // -c:v libx264 -r 30 -pix_fmt yuv420p  → common encoding settings
        string ffmpegArgs = $"-y -framerate 1 -i \"{Path.Combine(imagesDir, "image_%03d.png")}\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{videoPath}\"";

        var startInfo = new ProcessStartInfo
        {
            FileName = ffmpegExe,
            Arguments = ffmpegArgs,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        try
        {
            using (Process ffmpeg = Process.Start(startInfo))
            {
                string stdOut = ffmpeg.StandardOutput.ReadToEnd();
                string stdErr = ffmpeg.StandardError.ReadToEnd();
                ffmpeg.WaitForExit();

                Console.WriteLine(stdOut);
                if (ffmpeg.ExitCode != 0)
                {
                    Console.Error.WriteLine($"FFmpeg exited with code {ffmpeg.ExitCode}");
                    Console.Error.WriteLine(stdErr);
                }
                else
                {
                    Console.WriteLine($"Video slideshow created successfully at: {videoPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error executing FFmpeg: {ex.Message}");
        }
    }
}