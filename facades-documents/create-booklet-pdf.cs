using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class BookletCreator
{
    /// <summary>
    /// Creates a booklet PDF from the provided PDF stream using Aspose.Pdf.Facades.
    /// The result is returned as a MemoryStream positioned at the beginning.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF.</param>
    /// <returns>MemoryStream with the booklet PDF.</returns>
    public static MemoryStream CreateBookletPdf(Stream inputPdfStream)
    {
        // Ensure the input stream is at the beginning
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Temporary file paths for the source and the generated booklet
        string tempInputPath = Path.GetTempFileName();
        string tempOutputPath = Path.GetTempFileName();

        // Write the input PDF stream to a temporary file
        using (FileStream tempInputFile = new FileStream(tempInputPath, FileMode.Create, FileAccess.Write))
        {
            inputPdfStream.CopyTo(tempInputFile);
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        // Create the booklet PDF; use the correct MakeBooklet overload (no PageSize argument)
        bool success = editor.MakeBooklet(tempInputPath, tempOutputPath);
        if (!success)
            throw new InvalidOperationException("Failed to create booklet PDF.");

        // Read the generated booklet into a MemoryStream
        MemoryStream bookletStream = new MemoryStream();
        using (FileStream tempOutputFile = new FileStream(tempOutputPath, FileMode.Open, FileAccess.Read))
        {
            tempOutputFile.CopyTo(bookletStream);
        }

        // Reset the position so the caller can read from the start
        bookletStream.Position = 0;

        // Clean up temporary files
        try { File.Delete(tempInputPath); } catch { /* ignore cleanup errors */ }
        try { File.Delete(tempOutputPath); } catch { /* ignore cleanup errors */ }

        return bookletStream;
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via BookletCreator.
    }
}
