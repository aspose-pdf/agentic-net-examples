using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string bmpPath = "highres.bmp";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(bmpPath))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpPath}");
            return;
        }

        // Load the PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Replace the first image (index 1) on page 1 with the BMP file
        editor.ReplaceImage(1, 1, bmpPath);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Image replaced and saved to '{outputPath}'.");
    }
}
