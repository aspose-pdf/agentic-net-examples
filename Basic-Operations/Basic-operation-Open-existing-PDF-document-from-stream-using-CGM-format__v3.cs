using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Open the CGM file as a read‑only stream
            using (FileStream cgmStream = File.OpenRead(cgmPath))
            {
                // Load options specific to CGM files
                CgmLoadOptions loadOptions = new CgmLoadOptions();

                // Convert the CGM stream into a PDF Document
                using (Document doc = new Document(cgmStream, loadOptions))
                {
                    // Persist the resulting PDF
                    doc.Save(outputPdf);
                }
            }

            Console.WriteLine($"CGM successfully converted to PDF: '{outputPdf}'");
        }
        catch (PdfException ex)
        {
            // Handles errors thrown by Aspose.Pdf during loading or saving
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}