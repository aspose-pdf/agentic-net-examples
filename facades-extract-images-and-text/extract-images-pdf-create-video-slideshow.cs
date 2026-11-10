using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagesDir = "extracted_images";
        const string ffmpegPath = "ffmpeg"; // assumes ffmpeg is in the system PATH
        const string videoPath = "slideshow.mp4";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the directory for extracted images exists
        Directory.CreateDirectory(imagesDir);

        // Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string imageFile = Path.Combine(imagesDir, $"image_{imageIndex:D3}.png");
            extractor.GetNextImage(imageFile);
            imageIndex++;
        }

        if (imageIndex == 1)
        {
            Console.WriteLine("No images were found in the PDF.");
            return;
        }

        // Build FFmpeg arguments to create a slideshow (1 frame per second)
        string args = $"-y -framerate 1 -i \"{Path.Combine(imagesDir, "image_%03d.png")}\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{videoPath}\"";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = args,
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

                Console.WriteLine(stdOut);
                if (proc.ExitCode != 0)
                {
                    Console.Error.WriteLine($"FFmpeg exited with code {proc.ExitCode}");
                    Console.Error.WriteLine(stdErr);
                }
                else
                {
                    Console.WriteLine($"Video slideshow created: {videoPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error running FFmpeg: {ex.Message}");
        }
    }
}