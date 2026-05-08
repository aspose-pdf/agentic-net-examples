using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to be converted to A4 size
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        foreach (var inputPath in inputFiles)
        {
            // Verify the source file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build the output file name (e.g., doc1_A4.pdf)
            string directory = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_A4.pdf");

            // Retrieve A4 page dimensions (points)
            double a4Width = PageSize.A4.Width;
            double a4Height = PageSize.A4.Height;

            // Use PdfFileEditor to resize all pages to A4
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.ResizeContents(inputPath, outputPath, null, a4Width, a4Height);

            if (result)
                Console.WriteLine($"Successfully converted to A4: {outputPath}");
            else
                Console.Error.WriteLine($"Conversion failed for: {inputPath}");
        }
    }
}