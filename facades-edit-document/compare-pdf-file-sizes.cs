using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPdfPath = "original.pdf";
        const string editedPdfPath   = "edited.pdf";

        if (!File.Exists(originalPdfPath))
        {
            Console.Error.WriteLine($"Original file not found: {originalPdfPath}");
            return;
        }

        if (!File.Exists(editedPdfPath))
        {
            Console.Error.WriteLine($"Edited file not found: {editedPdfPath}");
            return;
        }

        // Load PDF files using Aspose.Pdf.Facades (PdfFileInfo)
        using (PdfFileInfo originalInfo = new PdfFileInfo(originalPdfPath))
        using (PdfFileInfo editedInfo   = new PdfFileInfo(editedPdfPath))
        {
            // Retrieve file sizes via System.IO.FileInfo (cross‑platform)
            long originalSize = new FileInfo(originalPdfPath).Length;
            long editedSize   = new FileInfo(editedPdfPath).Length;

            Console.WriteLine($"Original PDF size: {originalSize} bytes");
            Console.WriteLine($"Edited   PDF size: {editedSize} bytes");

            if (originalSize == editedSize)
                Console.WriteLine("File sizes are identical – no changes detected.");
            else
                Console.WriteLine("File sizes differ – changes have been applied.");
        }
    }
}