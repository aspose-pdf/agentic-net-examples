using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        {
            // Create the PdfPageEditor facade and bind the stream
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(pdfStream);

                // Example manipulation: rotate the first page 90 degrees
                editor.ProcessPages = new int[] { 1 }; // edit only page 1
                editor.Rotation = 90;                  // valid rotation values: 0, 90, 180, 270
                editor.ApplyChanges();

                // Save the edited PDF to a file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}