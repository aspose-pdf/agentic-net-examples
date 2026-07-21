using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_xmp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Retrieve existing XMP metadata
            byte[] existingMetadata = xmp.GetXmpMetadata();

            // If no metadata is present, add minimal XMP entries
            if (existingMetadata == null || existingMetadata.Length == 0)
            {
                // Add a simple title
                xmp.Add("dc:title", "Untitled Document");
                // Add a creator entry
                xmp.Add("dc:creator", "Aspose.Pdf");
                // Add creation date in ISO 8601 format
                xmp.Add("xmp:CreateDate", DateTime.UtcNow.ToString("o"));
            }

            // Save the PDF with the (new) XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}