using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXmp = "output.xmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        // Load the PDF into the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);
        byte[] metadataBytes = xmp.GetXmpMetadata();

        // Write the XMP metadata to a side‑car file
        using (FileStream fs = new FileStream(outputXmp, FileMode.Create, FileAccess.Write))
        {
            fs.Write(metadataBytes, 0, metadataBytes.Length);
        }

        Console.WriteLine("XMP metadata exported to " + outputXmp);
    }
}
