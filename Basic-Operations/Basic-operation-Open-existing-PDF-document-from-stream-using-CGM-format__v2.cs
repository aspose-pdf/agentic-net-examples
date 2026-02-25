using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Open the CGM file as a stream and load it with CGM-specific options.
            using (FileStream cgmStream = File.OpenRead(cgmPath))
            {
                CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 300dpi page size
                using (Document doc = new Document(cgmStream, loadOptions))
                {
                    // Save the resulting PDF document.
                    doc.Save(pdfPath);
                }
            }

            Console.WriteLine($"CGM successfully converted to PDF: '{pdfPath}'");
        }
        catch (PdfException ex)
        {
            // Handles PDF-related errors (e.g., malformed CGM input).
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}