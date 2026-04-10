using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";                 // Source PDF
        const string imagesDir = "extracted_images";         // Folder for extracted pictures
        const string ffmpegPath = "ffmpeg";                  // Assumes ffmpeg is in system PATH
        const string outputVideo = "slideshow.mp4";          // Resulting video file

        // Verify the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the directory for images exists
        Directory.CreateDirectory(imagesDir);

        // -----------------------------------------------------------------
        // Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Instruct the extractor to pull out images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each image sequentially
            while (extractor.HasNextImage())
            {
                // Name images with leading zeros for proper ordering (e.g., img_0001.jpg)
                string imagePath = Path.Combine(imagesDir, $"img_{imageIndex:D4}.jpg");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        // ---------------------------------------------------------------
        // Build FFmpeg command line to create a video from the images
        // ---------------------------------------------------------------
        // -y               : overwrite output file if it exists
        // -framerate 1     : display each image for 1 second
        // -i img_%04d.jpg  : input pattern matching the extracted images
        // -c:v libx264    : encode using H.264
        // -r 30            : output frame rate
        // -pix_fmt yuv420p : pixel format for broad compatibility
        string inputPattern = Path.Combine(imagesDir, "img_%04d.jpg");
        string ffmpegArgs = $"-y -framerate 1 -i \"{inputPattern}\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{outputVideo}\"";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = ffmpegArgs,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // ---------------------------------------------------------------
        // Execute FFmpeg and capture its output
        // ---------------------------------------------------------------
        try
        {
            using (Process? proc = Process.Start(startInfo))
            {
                if (proc == null)
                {
                    Console.Error.WriteLine("Failed to start FFmpeg process.");
                    return;
                }

                string stdout = proc.StandardOutput.ReadToEnd();
                string stderr = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                Console.WriteLine(stdout);
                if (proc.ExitCode != 0)
                {
                    Console.Error.WriteLine($"FFmpeg exited with code {proc.ExitCode}");
                    Console.Error.WriteLine(stderr);
                }
                else
                {
                    Console.WriteLine($"Video created successfully: {outputVideo}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error running FFmpeg: {ex.Message}");
        }
    }
}
