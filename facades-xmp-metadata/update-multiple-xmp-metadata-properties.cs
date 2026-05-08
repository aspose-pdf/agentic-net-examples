using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdf))
        {
            using (var doc = new Document())
            {
                // Add a blank page so the file is a valid PDF.
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Bind the XMP metadata facade to the source PDF.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Add multiple XMP properties in a single transaction.
            // Aspose.Pdf writes all changes atomically when Save is called.
            xmp.Add("xmp:CreatorTool", "MyApplication");
            xmp.Add("xmp:CreateDate", DateTime.UtcNow);
            xmp.Add("xmp:Nickname", "SampleDocument");
            xmp.Add("xmp:ModifyDate", DateTime.UtcNow);
            xmp.Add("xmp:MetadataDate", DateTime.UtcNow);

            // Save the PDF with the updated XMP metadata atomically.
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"XMP metadata updated and saved to '{outputPdf}'.");
    }
}
