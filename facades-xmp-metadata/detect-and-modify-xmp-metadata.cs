using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the XMP metadata facade and bind the PDF (load)
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Retrieve the XMP metadata as a byte array
            byte[] data = xmp.GetXmpMetadata();

            // Determine if XMP metadata exists (non‑empty byte array)
            bool hasXmp = data != null && data.Length > 0;
            Console.WriteLine($"XMP metadata present: {hasXmp}");

            if (hasXmp)
            {
                // Example modification: add a custom XMP property
                xmp.Add("xmp:Nickname", "MyDocument");

                // Save the modified PDF (save)
                xmp.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to {outputPath}");
            }
            else
            {
                Console.WriteLine("No XMP metadata; no modifications performed.");
            }
        }
    }
}