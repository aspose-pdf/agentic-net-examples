using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath  = "company_logo.bmp";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // PdfFileStamp does not implement IDisposable, so we instantiate it directly
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Bind the source PDF file
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that contains the header image
        using (FileStream imgStream = File.OpenRead(logoPath))
        {
            // Configure the Facades stamp
            Aspose.Pdf.Facades.Stamp headerStamp = new Aspose.Pdf.Facades.Stamp();
            headerStamp.BindImage(imgStream);          // use the image stream as the stamp content
            headerStamp.SetOrigin(0, 12);               // top margin of 12 points (origin is measured from the bottom‑left corner)
            // By default the stamp is applied to all pages; no need to set the Pages property

            // Apply the stamp to the PDF
            fileStamp.AddStamp(headerStamp);
        }

        // Save the stamped PDF
        fileStamp.Save(outputPdf);

        // Release internal resources
        fileStamp.Close();
    }
}
