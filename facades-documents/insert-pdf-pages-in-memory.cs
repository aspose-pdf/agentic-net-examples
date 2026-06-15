using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    // Entry point required for a console application
    public static void Main(string[] args)
    {
        // Example file paths – replace with actual locations or streams as needed
        const string destinationPdfPath = "destination.pdf";
        const string sourcePdfPath      = "source.pdf";

        // Insert after page 1 (1‑based index) and take pages 2 and 3 from the source PDF
        int insertLocation = 1;
        int[] pagesToInsert = new int[] { 2, 3 };

        try
        {
            using (MemoryStream result = InsertPagesInMemory(
                       destinationPdfPath,
                       sourcePdfPath,
                       insertLocation,
                       pagesToInsert))
            {
                // The result MemoryStream now contains the merged PDF.
                // For demonstration, write its size to the console.
                Console.WriteLine($"Merged PDF size: {result.Length} bytes");

                // If you need to persist the result to a file, uncomment the line below:
                // File.WriteAllBytes("merged_output.pdf", result.ToArray());
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Inserts selected pages from a source PDF into a destination PDF.
    /// Both PDFs are loaded into memory streams, and the merged result is returned
    /// as a new MemoryStream.
    /// </summary>
    /// <param name="destPdfPath">Path to the destination PDF.</param>
    /// <param name="srcPdfPath">Path to the source PDF whose pages will be inserted.</param>
    /// <param name="insertLocation">1‑based position in the destination where pages will be inserted.</param>
    /// <param name="pageNumbers">Array of page numbers (1‑based) to take from the source PDF.</param>
    /// <returns>A MemoryStream containing the merged PDF.</returns>
    private static MemoryStream InsertPagesInMemory(
        string destPdfPath,
        string srcPdfPath,
        int insertLocation,
        int[] pageNumbers)
    {
        // Load the destination PDF completely into a memory stream
        MemoryStream destStream = new MemoryStream(File.ReadAllBytes(destPdfPath));

        // Load the source PDF completely into a memory stream
        MemoryStream srcStream = new MemoryStream(File.ReadAllBytes(srcPdfPath));

        // Prepare an output stream that will receive the merged PDF
        MemoryStream outputStream = new MemoryStream();

        // Use PdfFileEditor.TryInsert to perform the operation without throwing exceptions
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.TryInsert(
            destStream,          // input PDF (destination)
            insertLocation,      // where to insert in the destination
            srcStream,           // PDF providing pages to insert
            pageNumbers,         // which pages from the source to insert
            outputStream);       // resulting PDF

        // Reset the position of the output stream so it can be read from the beginning
        outputStream.Position = 0;

        // Clean up the input streams; the output stream is returned to the caller
        destStream.Dispose();
        srcStream.Dispose();

        if (!success)
        {
            outputStream.Dispose();
            throw new InvalidOperationException("PdfFileEditor.TryInsert reported failure.");
        }

        return outputStream;
    }
}