using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfProcessor
{
    public static MemoryStream DeletePagesAndResize(string inputPath, int[] pagesToDelete, double newWidth, double newHeight)
    {
        if (string.IsNullOrEmpty(inputPath))
        {
            throw new ArgumentException("Input path cannot be null or empty.", nameof(inputPath));
        }

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException("Input PDF file not found.", inputPath);
        }

        // Open the source PDF file for reading
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream deletedStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages
            editor.Delete(sourceStream, pagesToDelete, deletedStream);

            // Prepare the deleted stream for the next operation
            deletedStream.Position = 0;

            // Stream that will hold the final resized PDF
            MemoryStream resultStream = new MemoryStream();

            // Resize contents of all pages (null means all pages)
            editor.TryResizeContents(deletedStream, resultStream, null, newWidth, newHeight);

            // Reset the result stream position before returning to the caller
            resultStream.Position = 0;

            return resultStream;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration – not required for library functionality.
        // Example:
        // if (args.Length > 0)
        // {
        //     var stream = PdfProcessor.DeletePagesAndResize(args[0], new int[] { 1, 2 }, 595, 842);
        //     // Use the resulting stream as needed.
        // }
    }
}