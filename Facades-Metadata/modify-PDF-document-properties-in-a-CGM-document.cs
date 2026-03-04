using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath     = "input.cgm";
        const string intermediatePdfPath = "intermediate.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Convert the CGM file to PDF using the Facade producer.
        try
        {
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, intermediatePdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"CGM to PDF conversion failed: {ex.Message}");
            return;
        }

        // Modify PDF document properties using PdfFileInfo facade.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(intermediatePdfPath))
        {
            pdfInfo.Title    = "Converted PDF Document";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "PDF generated from CGM";
            pdfInfo.Keywords = "CGM, PDF, Aspose.Pdf.Facades";

            // Save the updated PDF to the final output file.
            pdfInfo.SaveNewInfo(outputPdfPath);
        }

        Console.WriteLine($"PDF created with updated properties: {outputPdfPath}");
    }
}