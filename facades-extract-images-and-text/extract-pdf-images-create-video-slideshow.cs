using System;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "extracted_images";
        const string outputVideoPath = "slideshow.mp4";

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        // -----------------------------------------------------------------
        // Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Perform the extraction (no need to set ExtractImageMode – the method does it)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a zero‑padded file name (e.g., img0001.png)
                string imagePath = Path.Combine(
                    imagesFolder,
                    $"img{imageIndex:D4}.png");

                // Save the next image as PNG
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                imageIndex++;
            }
        }

        // ---------------------------------------------------------------
        // Create a video slideshow from the extracted images using FFmpeg
        // ---------------------------------------------------------------
        // FFmpeg command:
        //   -y                : overwrite output file if it exists
        //   -framerate 1      : 1 frame per second (adjust as needed)
        //   -i "img%04d.png"  : input pattern matching the saved images
        //   -c:v libx264      : encode using H.264
        //   -r 30             : output frame rate
        //   -pix_fmt yuv420p  : pixel format for broad compatibility
        string ffmpegArgs = $"-y -framerate 1 -i \"{Path.Combine(imagesFolder, "img%04d.png")}\" " +
                            $"-c:v libx264 -r 30 -pix_fmt yuv420p \"{outputVideoPath}\"";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = ffmpegArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using (Process? ffmpeg = Process.Start(startInfo))
            {
                if (ffmpeg == null)
                {
                    Console.Error.WriteLine("Failed to start FFmpeg process.");
                    return;
                }

                // Capture any output for debugging purposes
                string stdout = ffmpeg.StandardOutput.ReadToEnd();
                string stderr = ffmpeg.StandardError.ReadToEnd();

                ffmpeg.WaitForExit();

                if (ffmpeg.ExitCode == 0)
                {
                    Console.WriteLine($"Video created successfully: {outputVideoPath}");
                }
                else
                {
                    Console.Error.WriteLine("FFmpeg reported an error:");
                    Console.Error.WriteLine(stderr);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to start FFmpeg: {ex.Message}");
        }
    }
}
