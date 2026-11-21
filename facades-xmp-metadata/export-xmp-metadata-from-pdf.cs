using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmpPath = "output.xmp";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        PdfXmpMetadata xmpFacade = new PdfXmpMetadata();
        xmpFacade.BindPdf(inputPdfPath);

        // Retrieve the XMP packet as a byte array (may be null if none exists)
        byte[] xmpBytes = xmpFacade.GetXmpMetadata();

        // Convert the byte array to a UTF‑8 string; if null, use an empty string
        string xmpContent = xmpBytes != null ? Encoding.UTF8.GetString(xmpBytes) : string.Empty;

        // Write the XMP data to a side‑car file
        File.WriteAllText(outputXmpPath, xmpContent);
        Console.WriteLine($"XMP metadata exported to '{outputXmpPath}'.");
    }
}
