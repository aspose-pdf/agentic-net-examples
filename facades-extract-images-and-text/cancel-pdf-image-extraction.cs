using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputText = "output.txt";
        const string outputImageDir = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var cts = new CancellationTokenSource();

        // Example cancellation trigger (after 5 seconds)
        var cancelThread = new Thread(() =>
        {
            Thread.Sleep(5000);
            Console.WriteLine("Cancellation requested.");
            cts.Cancel();
        });
        cancelThread.Start();

        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file
                extractor.BindPdf(inputPath);

                // Optional: limit to specific pages (1‑based indexing)
                extractor.StartPage = 1;
                // extractor.EndPage can be set if a range is needed

                // Abort if cancellation requested before any extraction
                if (cts.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Operation cancelled before extraction.");
                    return;
                }

                // Extract text
                extractor.ExtractText();

                if (cts.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Operation cancelled after text extraction.");
                    return;
                }

                // Save extracted text directly to file (GetText overload expects output path)
                extractor.GetText(outputText);

                // Extract images (no need to set ExtractImageMode – the method starts extraction)
                extractor.ExtractImage();

                if (cts.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Operation cancelled after image extraction.");
                    return;
                }

                // Save extracted images
                Directory.CreateDirectory(outputImageDir);
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation cancelled during image saving.");
                        break;
                    }

                    string imgPath = Path.Combine(outputImageDir, $"Image_{imageIndex}.png");
                    using (FileStream imgStream = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                    {
                        extractor.GetNextImage(imgStream);
                    }
                    imageIndex++;
                }
            }

            Console.WriteLine("Extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
