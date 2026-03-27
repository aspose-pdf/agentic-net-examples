using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfPageDeleter
{
    /// <summary>
    /// Deletes the specified pages from a PDF and returns the size (in bytes) of the resulting document.
    /// The operation is performed completely in memory – no temporary files are created on disk.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF file.</param>
    /// <param name="pageNumbers">Array of 1‑based page numbers to delete.</param>
    /// <returns>Size of the resulting PDF in bytes.</returns>
    public static long DeletePagesAndGetSize(string inputPdfPath, int[] pageNumbers)
    {
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be a non‑empty string.", nameof(inputPdfPath));

        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"The PDF file '{inputPdfPath}' could not be found.", inputPdfPath);

        // Use the stream‑based overload of PdfFileEditor.Delete to avoid writing a temporary file.
        using (FileStream inputStream = File.OpenRead(inputPdfPath))
        using (MemoryStream outputStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputStream, pageNumbers, outputStream);

            // After Delete completes, the outputStream contains the new PDF.
            return outputStream.Length;
        }
    }

    // Example usage of the method
    public static void Main()
    {
        string pdfPath = "sample.pdf"; // Ensure this file exists in the working directory.
        int[] pagesToDelete = new int[] { 2, 3 };

        try
        {
            long resultingSize = DeletePagesAndGetSize(pdfPath, pagesToDelete);
            Console.WriteLine($"Resulting PDF size (bytes): {resultingSize}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
