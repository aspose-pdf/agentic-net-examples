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

        // Load the PDF, replace its XMP metadata, and save to a temporary file
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            using (FileStream xmpStream = File.OpenRead(xmpFilePath))
            {
                // Replace the existing XMP block with the external XMP data
                pdfDoc.SetXmpMetadata(xmpStream);
            }

            // Save the modified PDF to a temporary location
            string tempPdfPath = Path.GetTempFileName();
            pdfDoc.Save(tempPdfPath);

            // Use the Facades API (PdfFileInfo) to write the new XMP block into the final output
            PdfFileInfo fileInfo = new PdfFileInfo();
            fileInfo.BindPdf(tempPdfPath);
            bool saved = fileInfo.SaveNewInfoWithXmp(outputPdfPath);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save the PDF with the new XMP metadata.");
            }

            // Clean up the temporary file
            File.Delete(tempPdfPath);
        }

        Console.WriteLine($"PDF with replaced XMP metadata saved to '{outputPdfPath}'.");
    }
}