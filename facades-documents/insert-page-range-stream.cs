using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string insertPath = "insert.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(insertPath))
        {
            Console.Error.WriteLine($"Insert file not found: {insertPath}");
            return;
        }

        using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
        using (FileStream insertStream = new FileStream(insertPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            // Insert pages 2 through 4 from insert.pdf after page 1 of source.pdf
            bool success = editor.Insert(sourceStream, 1, insertStream, 2, 4, outputStream);
            Console.WriteLine(success ? $"Pages inserted successfully into {outputPath}" : "Insert operation failed.");
        }
    }
}