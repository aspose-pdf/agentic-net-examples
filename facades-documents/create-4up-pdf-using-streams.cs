using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Prepare an in‑memory stream to receive the N‑up result
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Instantiate the PdfFileEditor facade
                PdfFileEditor editor = new PdfFileEditor();

                // Apply a 4‑up layout (2 columns × 2 rows) using the stream overload
                // The method returns true on success
                bool success = editor.MakeNUp(inputStream, outputStream, 2, 2);

                if (!success)
                {
                    Console.Error.WriteLine("MakeNUp operation failed.");
                    return;
                }

                // Reset the output stream position for any subsequent reads
                outputStream.Position = 0;

                // (Optional) Write the in‑memory PDF to a file for verification
                File.WriteAllBytes("output_4up.pdf", outputStream.ToArray());

                Console.WriteLine("4‑up PDF created in memory successfully.");
            }
        }
    }
}