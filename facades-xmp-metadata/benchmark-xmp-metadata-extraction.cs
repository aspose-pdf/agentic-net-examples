using System;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "large_input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Create the XMP metadata facade
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Start timing
                Stopwatch sw = Stopwatch.StartNew();

                // Load the PDF file
                xmp.BindPdf(pdfPath);

                // Retrieve the full XMP metadata as a byte array
                byte[] data = xmp.GetXmpMetadata();

                // Stop timing
                sw.Stop();

                // Output benchmark result
                Console.WriteLine($"XMP metadata size: {data.Length} bytes");
                Console.WriteLine($"Time elapsed: {sw.ElapsedMilliseconds} ms");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}