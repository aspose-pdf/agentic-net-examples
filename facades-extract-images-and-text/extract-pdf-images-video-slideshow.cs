using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "input.pdf";                     // PDF to extract images from
        const string imagesDir = "ExtractedImages";            // Folder to store extracted pictures
        const string videoPath = "slideshow.mp4";              // Output video file
        const string ffmpegExe = "ffmpeg";                     // Assumes ffmpeg is in the system PATH

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create the output directory for images
        Directory.CreateDirectory(imagesDir);

        // -----------------------------------------------------------------
        // 1. Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Enable image extraction mode – use the method, not the property
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a zero‑padded file name (e.g., image_001.jpg)
                string imageFile = Path.Combine(
                    imagesDir,
                    $"image_{imageIndex:D3}.jpg");

                // Save the next image as JPEG
                extractor.GetNextImage(imageFile, ImageFormat.Jpeg);

                imageIndex++;
            }
        }

        // Verify that at least one image was extracted
        string[] extractedFiles = Directory.GetFiles(imagesDir, "image_*.jpg");
        if (extractedFiles.Length == 0)
        {
            Console.Error.WriteLine("No images were extracted from the PDF.");
            return;
        }

        // ---------------------------------------------------------------
        // 2. Create a video slideshow from the extracted images using FFmpeg
        // ---------------------------------------------------------------
        // Build the FFmpeg arguments:
        // -y               : overwrite output file without asking
        // -framerate 1     : 1 frame per second (adjust as needed)
        // -i image_%03d.jpg: input pattern matching the extracted files
        // -c:v libx264    : encode video with H.264 codec
        // -r 30            : output frame rate
        // -pix_fmt yuv420p : pixel format compatible with most players
        string ffmpegArgs = $"-y -framerate 1 -i \"image_%03d.jpg\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{videoPath}\"";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = ffmpegExe,
            Arguments = ffmpegArgs,
            WorkingDirectory = Path.GetFullPath(imagesDir),
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        try
        {
            using (Process ffmpeg = Process.Start(startInfo))
            {
                // Capture any output for debugging purposes
                string stdout = ffmpeg.StandardOutput.ReadToEnd();
                string stderr = ffmpeg.StandardError.ReadToEnd();

                ffmpeg.WaitForExit();

                if (ffmpeg.ExitCode == 0)
                {
                    Console.WriteLine($"Video slideshow created successfully: {videoPath}");
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
