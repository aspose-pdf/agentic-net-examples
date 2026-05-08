using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF into a byte array
        byte[] inputBytes = File.ReadAllBytes(inputPath);

        // Edit the PDF entirely in memory
        byte[] outputBytes = EditPdfInMemory(inputBytes);

        // Write the edited PDF to the destination file
        File.WriteAllBytes(outputPath, outputBytes);
        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }

    // Edits a PDF using PdfPageEditor with in‑memory streams.
    // Example modifications: set zoom to 50% and rotate all pages 90°.
    static byte[] EditPdfInMemory(byte[] pdfData)
    {
        // Input stream containing the original PDF
        using (var inputStream = new MemoryStream(pdfData))
        {
            // PdfPageEditor works on the bound document
            using (var editor = new PdfPageEditor())
            {
                // Bind the PDF from the input stream
                editor.BindPdf(inputStream);

                // Apply desired edits
                editor.Zoom = 0.5f;      // 50 % zoom
                editor.Rotation = 90;   // Rotate pages 90 degrees

                // Output stream for the edited PDF
                using (var outputStream = new MemoryStream())
                {
                    // Save the modified PDF into the output stream
                    editor.Save(outputStream);

                    // Return the edited PDF as a byte array
                    return outputStream.ToArray();
                }
            }
        }
    }
}