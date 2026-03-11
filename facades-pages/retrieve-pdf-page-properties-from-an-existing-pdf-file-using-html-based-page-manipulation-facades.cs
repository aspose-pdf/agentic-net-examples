using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo provides meta‑information about the PDF, including per‑page data.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            // Ensure the facade is bound to the PDF file.
            fileInfo.BindPdf(inputPdf);

            // Basic document‑level properties.
            Console.WriteLine($"IsEncrypted : {fileInfo.IsEncrypted}");
            Console.WriteLine($"IsPdfFile  : {fileInfo.IsPdfFile}");
            Console.WriteLine($"Number of pages : {fileInfo.NumberOfPages}");
            Console.WriteLine($"Author     : {fileInfo.Author}");
            Console.WriteLine($"Title      : {fileInfo.Title}");
            Console.WriteLine();

            // Iterate over each page (1‑based indexing).
            for (int pageNum = 1; pageNum <= fileInfo.NumberOfPages; pageNum++)
            {
                // Retrieve page dimensions and rotation.
                double width  = fileInfo.GetPageWidth(pageNum);
                double height = fileInfo.GetPageHeight(pageNum);
                int rotation = fileInfo.GetPageRotation(pageNum);
                double xOffset = fileInfo.GetPageXOffset(pageNum);
                double yOffset = fileInfo.GetPageYOffset(pageNum);

                Console.WriteLine($"Page {pageNum}:");
                Console.WriteLine($"  Width   = {width} pt");
                Console.WriteLine($"  Height  = {height} pt");
                Console.WriteLine($"  Rotation= {rotation}°");
                Console.WriteLine($"  XOffset = {xOffset} pt");
                Console.WriteLine($"  YOffset = {yOffset} pt");
                Console.WriteLine();
            }
        }
    }
}