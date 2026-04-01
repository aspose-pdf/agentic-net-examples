using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Bind to the original PDF and log XMP block size
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);
            byte[] originalData = xmp.GetXmpMetadata();
            int originalSize = originalData.Length;
            Console.WriteLine("Original XMP block size: " + originalSize + " bytes");

            // Add a custom XMP property
            Aspose.Pdf.XmpValue newValue = new Aspose.Pdf.XmpValue("CustomValue");
            xmp.Add("xmp:CustomProperty", newValue);

            // Save the modified PDF
            xmp.Save(outputPath);
        }

        // Bind to the modified PDF and log new XMP block size
        using (PdfXmpMetadata xmpModified = new PdfXmpMetadata())
        {
            xmpModified.BindPdf(outputPath);
            byte[] modifiedData = xmpModified.GetXmpMetadata();
            int modifiedSize = modifiedData.Length;
            Console.WriteLine("Modified XMP block size: " + modifiedSize + " bytes");
        }
    }
}