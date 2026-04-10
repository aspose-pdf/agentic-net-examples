using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF operations

class PdfToByteArrayConverter
{
    // Converts a filled PDF file to a byte array without writing to disk.
    // The method uses Aspose.Pdf.Facades.PdfViewer to load the PDF and save it to a memory stream.
    public static byte[] ConvertPdfToByteArray(string pdfFilePath)
    {
        if (!File.Exists(pdfFilePath))
            throw new FileNotFoundException($"PDF file not found: {pdfFilePath}");

        // Initialize the viewer facade and bind the source PDF.
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(pdfFilePath);               // Load the PDF into the facade

            // Save the result PDF directly into a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                viewer.Save(ms);                       // Facade saves to the provided stream
                return ms.ToArray();                   // Return the underlying byte array
            }
        }
        finally
        {
            // Ensure resources are released.
            viewer.Close();
        }
    }

    // Example usage
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