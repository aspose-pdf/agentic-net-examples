using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // MemoryStream that will hold the resulting 4‑up PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Open the source PDF as a read‑only stream
            using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Create the PdfFileEditor facade (it does not implement IDisposable)
                PdfFileEditor editor = new PdfFileEditor();

                // Apply a 4‑up layout: 2 columns × 2 rows
                bool success = editor.MakeNUp(inputStream, outputStream, 2, 2);

                if (!success)
                {
                    Console.Error.WriteLine("MakeNUp operation failed.");
                    return;
                }
            }

            // Reset the position so the stream can be read from the beginning
            outputStream.Position = 0;

            // The PDF is now fully contained in 'outputStream'.
            // Example: obtain the byte array (optional)
            byte[] pdfBytes = outputStream.ToArray();
            Console.WriteLine($"4‑up PDF created in memory. Size: {pdfBytes.Length} bytes.");

            // Optional: write the memory stream to a physical file for verification
            // const string outputPath = "output_4up.pdf";
            // using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            // {
            //     outputStream.CopyTo(fileOut);
            // }
        }
    }
}