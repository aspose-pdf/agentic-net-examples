using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";
        const string logPath = "conversion.log";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file and convert it to a PDF document
            using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
            {
                // Convert the PDF to PDF/E (implemented as PDF/X‑3) format
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/E document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"CGM successfully converted to PDF/E and saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}