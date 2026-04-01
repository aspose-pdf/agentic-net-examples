using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string bmpImagePath = "highres.bmp";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }
        if (!File.Exists(bmpImagePath))
        {
            Console.Error.WriteLine("BMP image not found: " + bmpImagePath);
            return;
        }

        // Create a PdfContentEditor facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // Replace the first image (index 1) on the first page (pageNumber 1) with the BMP file
        editor.ReplaceImage(1, 1, bmpImagePath);

        // Save the modified PDF
        editor.Save(outputPdfPath);
        editor.Close();

        Console.WriteLine("Image replaced successfully. Output saved to " + outputPdfPath);
    }
}
