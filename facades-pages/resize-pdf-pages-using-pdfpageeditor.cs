using System;
using System.IO;
using Aspose.Pdf;               // PageSize enum lives here
using Aspose.Pdf.Facades;      // PdfPageEditor lives here

class PdfPageSizeModifier
{
    /// <summary>
    /// Loads a PDF from a byte array, changes all pages to the specified size,
    /// and saves the result to a file.
    /// </summary>
    /// <param name="pdfBytes">PDF content as a byte array.</param>
    /// <param name="outputPath">Path of the output PDF file.</param>
    /// <param name="newSize">Desired page size (e.g., PageSize.A4).</param>
    public static void ProcessPdf(byte[] pdfBytes, string outputPath, PageSize newSize)
    {
        if (pdfBytes == null || pdfBytes.Length == 0)
            throw new ArgumentException("Input PDF byte array is empty.", nameof(pdfBytes));

        // Wrap the byte array in a MemoryStream for the facade.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF stream to the editor.
            editor.BindPdf(inputStream);

            // Set the desired output page size.
            editor.PageSize = newSize;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF to the specified file.
            editor.Save(outputPath);
        }
    }

    // Helper: creates a minimal PDF in memory (one blank page).
    private static byte[] CreateSamplePdf()
    {
        using (var doc = new Document())
        {
            doc.Pages.Add(); // add a single blank page
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }

    // Example usage.
    static void Main()
    {
        // Instead of reading a physical file, generate a sample PDF in memory.
        byte[] pdfData = CreateSamplePdf();

        // Define output path and target page size.
        string resultPath = "output_resized.pdf";
        PageSize targetSize = PageSize.A4; // Change as needed (Letter, Legal, etc.)

        // Perform the modification.
        ProcessPdf(pdfData, resultPath, targetSize);

        Console.WriteLine($"PDF saved with new page size to '{resultPath}'.");
    }
}
