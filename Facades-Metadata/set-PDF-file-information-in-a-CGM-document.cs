using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string intermediatePdf = "intermediate.pdf";
        const string finalPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Convert CGM to PDF using PdfProducer
        try
        {
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, intermediatePdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting CGM to PDF: {ex.Message}");
            return;
        }

        // Set PDF file information using PdfFileInfo facade
        try
        {
            using (PdfFileInfo pdfInfo = new PdfFileInfo(intermediatePdf))
            {
                // Standard metadata
                pdfInfo.Title = "Sample PDF from CGM";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Demonstration of CGM to PDF conversion";
                pdfInfo.Keywords = "CGM, PDF, Aspose";
                pdfInfo.Creator = "Aspose.Pdf.Facades PdfProducer";

                // Custom metadata (optional)
                pdfInfo.SetMetaInfo("CustomProperty", "CustomValue");

                // Save updated PDF
                pdfInfo.SaveNewInfo(finalPdf);
            }

            Console.WriteLine($"PDF with updated metadata saved to '{finalPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating PDF metadata: {ex.Message}");
        }
    }
}