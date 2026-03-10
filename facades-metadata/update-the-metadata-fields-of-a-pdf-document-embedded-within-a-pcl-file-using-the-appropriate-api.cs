using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPclPath = "input.pcl";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPclPath}");
            return;
        }

        // Load the PCL file (PCL is input‑only) and obtain a PDF Document object.
        using (Document pdfDoc = new Document(inputPclPath, new PclLoadOptions()))
        {
            // Use the PdfFileInfo facade to edit the PDF metadata.
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfDoc))
            {
                // Update desired metadata fields.
                pdfInfo.Title = "Updated Title";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Updated Subject";
                pdfInfo.Keywords = "Aspose, PDF, Metadata";
                pdfInfo.Creator = "My Application";
                // ModDate expects a PDF‑formatted date string, not a DateTime.
                pdfInfo.ModDate = "D:" + DateTime.Now.ToString("yyyyMMddHHmmss");

                // Persist the changes. SaveNewInfo writes the PDF with the new metadata.
                pdfInfo.SaveNewInfo(outputPdfPath);
            }
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPdfPath}'.");
    }
}
