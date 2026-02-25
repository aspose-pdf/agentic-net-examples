using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file using CgmLoadOptions (CGM is input‑only)
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            using (Document doc = new Document(cgmPath, loadOptions))
            {
                // Save the resulting PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"CGM converted to PDF: '{pdfPath}'");
        }
        catch (PdfException ex)          // Handles PDF‑related errors
        {
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)            // Handles any other errors (e.g., invalid CGM)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}