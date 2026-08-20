using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the input PDF as a read‑only stream and prepare an output memory stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Instantiate the PdfFileEditor facade (does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Apply a 4‑up layout: 2 columns (x) and 2 rows (y)
            bool result = editor.TryMakeNUp(inputStream, outputStream, 2, 2);

            if (!result)
            {
                Console.Error.WriteLine("Failed to create the 4‑up PDF.");
                return;
            }

            // Reset the output stream position for any subsequent reads
            outputStream.Position = 0;

            // Example: persist the memory stream to a physical file (optional)
            using (FileStream fileOut = new FileStream("output_4up.pdf", FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(fileOut);
            }

            Console.WriteLine("4‑up PDF created in memory and saved to 'output_4up.pdf'.");
        }
    }
}