using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputCgm = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputCgm))
        {
            Console.Error.WriteLine($"File not found: {inputCgm}");
            return;
        }

        try
        {
            // Default CGM load options (A4 page size, 300 DPI)
            CgmLoadOptions loadOptions = new CgmLoadOptions();

            // Load the CGM file and convert it to a PDF document
            using (Document doc = new Document(inputCgm, loadOptions))
            {
                // Save the converted PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"CGM successfully converted to PDF: '{outputPdf}'");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}