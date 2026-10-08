using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the PDF file on a network share
        const string pdfPath = @"\\server\share\folder\document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document first – PdfXmpMetadata expects a Document instance.
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Pass the Document to the PdfXmpMetadata facade.
                using (PdfXmpMetadata xmp = new PdfXmpMetadata(pdfDoc))
                {
                    // GetXmpMetadata returns a byte[]; convert it to a UTF‑8 string for display.
                    byte[] xmpBytes = xmp.GetXmpMetadata();
                    string xmpMetadata = xmpBytes != null && xmpBytes.Length > 0
                        ? Encoding.UTF8.GetString(xmpBytes)
                        : string.Empty;

                    if (string.IsNullOrEmpty(xmpMetadata))
                    {
                        Console.WriteLine("No XMP metadata found in the PDF.");
                    }
                    else
                    {
                        Console.WriteLine("XMP Metadata:");
                        Console.WriteLine(xmpMetadata);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XMP metadata: {ex.Message}");
        }
    }
}
