using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for FontStyles and TextState

class Program
{
    // Async entry point
    static async Task Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Perform the PDF modification without blocking the calling thread
        await ModifyPdfAsync(inputPath, outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }

    // Wrap PDF modification in Task.Run for non‑blocking execution
    static Task ModifyPdfAsync(string sourcePath, string destinationPath)
    {
        return Task.Run(() =>
        {
            // Load the source PDF using the core Document API (PdfFileMend does not support stamping)
            var doc = new Document(sourcePath);

            // Create a text stamp that will be applied to every page
            var stamp = new TextStamp("Confidential")
            {
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Configure appearance
                Opacity = 0.3
                // Rotation is optional; the default is 0 degrees, so the unsupported RotationAngle enum is omitted
            };

            // Configure text appearance
            stamp.TextState.FontSize = 72;
            stamp.TextState.FontStyle = FontStyles.Bold; // Fixed: FontStyles now resolved via Aspose.Pdf.Text
            stamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // Red color, cross‑platform

            // Apply the stamp to each page (Aspose.Pdf uses 1‑based page indexing internally)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(destinationPath);
        });
    }
}
