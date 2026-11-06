using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class BookletCreator
{
    /// <summary>
    /// Creates a booklet PDF from the provided input PDF stream.
    /// The result is returned as a MemoryStream.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF.</param>
    /// <returns>MemoryStream with the booklet PDF.</returns>
    public static MemoryStream CreateBookletPdf(Stream inputPdfStream)
    {
        if (inputPdfStream == null)
            throw new ArgumentNullException(nameof(inputPdfStream));

        // Ensure the input stream is positioned at the beginning.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Write the incoming stream to a temporary file because PdfFileEditor works with file paths.
        string tempInputPath = Path.GetTempFileName();
        using (FileStream tempInputFile = File.Create(tempInputPath))
        {
            inputPdfStream.CopyTo(tempInputFile);
        }

        // Prepare a temporary file for the booklet output.
        string tempOutputPath = Path.GetTempFileName();

        // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Use the overload that accepts file paths (no PageSize argument required).
        bool success = editor.MakeBooklet(tempInputPath, tempOutputPath);
        if (!success)
            throw new InvalidOperationException("Failed to create booklet PDF.");

        // Load the generated booklet into a MemoryStream to return to the caller.
        MemoryStream outputStream = new MemoryStream();
        using (FileStream tempOutputFile = File.OpenRead(tempOutputPath))
        {
            tempOutputFile.CopyTo(outputStream);
        }
        outputStream.Position = 0;

        // Clean up temporary files.
        try { File.Delete(tempInputPath); } catch { /* ignore */ }
        try { File.Delete(tempOutputPath); } catch { /* ignore */ }

        return outputStream;
    }
}

// Dummy entry point to satisfy the compiler when building an executable project.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via BookletCreator.
    }
}