using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // Added for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagesDir = "extracted_images";
        const string ffmpegPath = "ffmpeg"; // Assumes ffmpeg is available in the system PATH
        const string outputVideo = "slideshow.mp4";

        // Verify the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory for images exists
        Directory.CreateDirectory(imagesDir);

        // -------------------------------------------------
        // Extract images from the PDF using Aspose.Pdf.Facades
        // -------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file
            extractor.BindPdf(inputPdf);

            // Perform the extraction (no need to set ExtractImageMode)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as a PNG file with a sequential name
                string imagePath = Path.Combine(imagesDir, $"img_{imageIndex:D4}.png");
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                imageIndex++;
            }
        }

        // -------------------------------------------------
        // Create a video slideshow from the extracted images using FFmpeg
        // -------------------------------------------------
        // Example: 1 frame per second, H.264 codec, 30 fps output
        string ffmpegArgs = $"-y -framerate 1 -i \"{Path.Combine(imagesDir, "img_%04d.png")}\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{outputVideo}\"";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = ffmpegArgs,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        try
        {
            Process? ffmpeg = Process.Start(startInfo);
            if (ffmpeg == null)
            {
                Console.Error.WriteLine("Failed to start FFmpeg process.");
                return;
            }

            ffmpeg.OutputDataReceived += (sender, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
            ffmpeg.ErrorDataReceived += (sender, e) => { if (e.Data != null) Console.Error.WriteLine(e.Data); };
            ffmpeg.BeginOutputReadLine();
            ffmpeg.BeginErrorReadLine();
            ffmpeg.WaitForExit();

            if (ffmpeg.ExitCode == 0)
            {
                Console.WriteLine($"Video slideshow created successfully: {outputVideo}");
            }
            else
            {
                Console.Error.WriteLine($"FFmpeg exited with code {ffmpeg.ExitCode}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to execute FFmpeg: {ex.Message}");
        }
    }
}
