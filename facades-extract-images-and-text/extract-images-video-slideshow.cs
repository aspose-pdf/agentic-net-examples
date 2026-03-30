using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputVideo = "slideshow.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a temporary folder to store extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        int imageIndex = 1;

        using (Document doc = new Document(inputPdf))
        {
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    string imagePath = Path.Combine(tempDir, $"img_{imageIndex:D4}.png");
                    // XImage.Save expects a Stream, not a file path string
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }
                    imageIndex++;
                }
            }
        }

        // Build FFmpeg arguments to create a video from the extracted images
        // -framerate 1 sets each image to display for 1 second
        // -c:v libx264 encodes using H.264, -r 30 sets output frame rate, -pix_fmt yuv420p ensures compatibility
        string ffmpegArgs = $"-y -framerate 1 -i \"{Path.Combine(tempDir, "img_%04d.png")}\" -c:v libx264 -r 30 -pix_fmt yuv420p \"{outputVideo}\"";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = ffmpegArgs,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using (Process process = new Process())
        {
            process.StartInfo = startInfo;
            process.Start();
            string stdOut = process.StandardOutput.ReadToEnd();
            string stdErr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(stdOut);
            if (!string.IsNullOrEmpty(stdErr))
            {
                Console.Error.WriteLine(stdErr);
            }
        }

        // Clean up temporary images
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }

        Console.WriteLine($"Video slideshow created: {outputVideo}");
    }
}
