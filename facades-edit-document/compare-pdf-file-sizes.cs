using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";

        if (!File.Exists(originalPath) || !File.Exists(editedPath))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Get file sizes using System.IO.FileInfo
        long originalSize = new FileInfo(originalPath).Length;
        long editedSize   = new FileInfo(editedPath).Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Edited size:   {editedSize} bytes");

        if (originalSize == editedSize)
            Console.WriteLine("File sizes are identical – no changes detected.");
        else
            Console.WriteLine("File sizes differ – changes have been applied.");

        // Verify that both files are valid PDFs using Aspose.Pdf.Facades
        using (PdfFileInfo infoOriginal = new PdfFileInfo(originalPath))
        using (PdfFileInfo infoEdited   = new PdfFileInfo(editedPath))
        {
            Console.WriteLine($"Original is PDF: {infoOriginal.IsPdfFile}");
            Console.WriteLine($"Edited is PDF:   {infoEdited.IsPdfFile}");
        }
    }
}