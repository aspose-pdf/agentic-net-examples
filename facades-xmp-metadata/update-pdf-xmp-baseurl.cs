using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        //   args[0] – path to the source PDF file
        //   args[1] – path where the updated PDF will be saved
        //   args[2] – (optional) BaseUrl to embed; if omitted a simple URL is built from the machine name.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <input.pdf> <output.pdf> [baseUrl]");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string baseUrl;

        if (args.Length >= 3)
        {
            baseUrl = args[2];
        }
        else
        {
            // Simulate the request URL (scheme + host) that would normally come from HttpContext.
            string scheme = "https";                     // default scheme
            string host   = Environment.MachineName;      // use the machine name as host
            baseUrl = $"{scheme}://{host}/";
        }

        // Update the XMP BaseUrl using Aspose.Pdf.Facades.PdfXmpMetadata.
        var xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);                                 // Load the PDF
        // Set (or replace) the BaseURL property.
        if (xmp.Contains(DefaultMetadataProperties.BaseURL))
        {
            xmp.Remove(DefaultMetadataProperties.BaseURL);
        }
        xmp.Add(DefaultMetadataProperties.BaseURL, new XmpValue(baseUrl));
        xmp.Save(outputPath);                                   // Save the modified PDF
        xmp.Close();

        Console.WriteLine($"XMP BaseUrl set to: {baseUrl}");
        Console.WriteLine($"Updated PDF written to: {outputPath}");
    }
}
