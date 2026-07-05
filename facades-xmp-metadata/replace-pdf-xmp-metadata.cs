using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmpFilePath   = "metadata.xmp";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmpFilePath))
        {
            Console.Error.WriteLine($"XMP file not found: {xmpFilePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Replace the existing XMP metadata with the external XMP file
            using (FileStream xmpStream = File.OpenRead(xmpFilePath))
            {
                pdfDoc.SetXmpMetadata(xmpStream);
            }

            // Save the updated PDF. Using PdfFileInfo facade to ensure the XMP block is written.
            PdfFileInfo fileInfo = new PdfFileInfo(inputPdfPath);
            bool saved = fileInfo.SaveNewInfoWithXmp(outputPdfPath);

            // Fallback to direct save if the facade method fails
            if (!saved)
            {
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with replaced XMP metadata to '{outputPdfPath}'.");
    }
}