using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(originalPath) || !File.Exists(editedPath))
        {
            Console.Error.WriteLine("Error: One or both PDF files were not found.");
            return;
        }

        // Optional: bind each PDF with the Facade to demonstrate usage of Aspose.Pdf.Facades
        PdfFileInfo originalInfo = new PdfFileInfo(originalPath);
        PdfFileInfo editedInfo   = new PdfFileInfo(editedPath);

        // Retrieve file sizes using System.IO.FileInfo (Aspose does not expose size directly)
        long originalSize = new FileInfo(originalPath).Length;
        long editedSize   = new FileInfo(editedPath).Length;

        Console.WriteLine($"Original PDF size: {originalSize} bytes");
        Console.WriteLine($"Edited PDF size:   {editedSize} bytes");

        // Simple comparison to confirm that changes affected the file size
        if (originalSize == editedSize)
        {
            Console.WriteLine("File sizes are identical – no changes detected.");
        }
        else
        {
            Console.WriteLine("File sizes differ – changes have been applied.");
        }
    }
}