using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Prepare a memory stream that will receive the 4‑up PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Open the source PDF as a read‑only stream
            using (FileStream inputFileStream = File.OpenRead(inputPath))
            {
                // PdfFileEditor does not implement IDisposable – instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // 4‑up layout = N‑up with n = 4.
                // In the Aspose.Pdf version used, the stream overload expects an int for orientation (0 = portrait, 1 = landscape).
                // Parameters: (inputStream, outputStream, nUp, orientation)
                editor.MakeNUp(inputFileStream, outputStream, 4, 0); // 0 = portrait
            }

            // Reset the position so the stream can be read from the beginning
            outputStream.Position = 0;

            // Optional: write the memory stream to a physical file for verification
            const string outputPath = "output_4up.pdf";
            File.WriteAllBytes(outputPath, outputStream.ToArray());

            Console.WriteLine($"4‑up PDF generated and saved to '{outputPath}'.");
        }
    }
}
