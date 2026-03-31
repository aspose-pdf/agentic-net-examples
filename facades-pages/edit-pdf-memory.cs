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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load source PDF into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Initialize PdfPageEditor and bind the input stream
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputStream);
                // Example modifications: rotate pages 90 degrees and set zoom to 50%
                editor.Rotation = 90;
                editor.Zoom = 0.5f;
                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the edited PDF into an output memory stream
                using (MemoryStream outputStream = new MemoryStream())
                {
                    editor.Save(outputStream);
                    // Write the resulting bytes to a file
                    File.WriteAllBytes(outputPath, outputStream.ToArray());
                    Console.WriteLine("Edited PDF saved to " + outputPath);
                }
            }
        }
    }
}