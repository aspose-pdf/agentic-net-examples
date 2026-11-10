using System;
using System.IO;
using Aspose.Pdf; // Document class for PDF manipulation

class PdfByteArrayConverter
{
    /// <summary>
    /// Loads a filled PDF from the specified file path and returns its content as a byte array.
    /// No file is written to disk; the PDF is saved into a MemoryStream.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <returns>Byte array containing the PDF data.</returns>
    public static byte[] ConvertPdfToByteArray(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Aspose.Pdf.Document can load a PDF and be saved directly to a stream.
        // This avoids the need for PdfDocumentProcessor which is not present in the
        // current Aspose.PDF package version.
        Document pdfDocument = new Document(pdfPath);

        using (MemoryStream ms = new MemoryStream())
        {
            pdfDocument.Save(ms);
            // Reset the stream position before reading the bytes.
            ms.Position = 0;
            return ms.ToArray();
        }
    }

    // Example usage:
    static void Main()
    {
        const string inputPdf = "filled_form.pdf";

        try
        {
            byte[] pdfBytes = ConvertPdfToByteArray(inputPdf);
            Console.WriteLine($"PDF converted to byte array, length = {pdfBytes.Length} bytes.");
            // The byte array can now be sent over a web API.
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
