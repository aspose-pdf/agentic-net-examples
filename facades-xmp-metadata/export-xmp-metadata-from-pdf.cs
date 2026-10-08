using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXmp = "output.xmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPdf);

            // Use the PdfXmpMetadata facade to retrieve the raw XMP packet
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata(doc))
            {
                byte[] xmpData = xmpFacade.GetXmpMetadata();

                if (xmpData == null || xmpData.Length == 0)
                {
                    Console.WriteLine("No XMP metadata found in the PDF.");
                }
                else
                {
                    // Write the XMP side‑car file
                    File.WriteAllBytes(outputXmp, xmpData);
                    Console.WriteLine($"XMP metadata exported to '{outputXmp}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
