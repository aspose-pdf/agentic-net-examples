using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source CGM file and the desired PDF output
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify that the CGM file exists before attempting to load it
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // CGM is an input‑only format; use CgmLoadOptions to load the file as a PDF document
            CgmLoadOptions loadOptions = new CgmLoadOptions();

            // Wrap the Document in a using block for deterministic disposal
            using (Document doc = new Document(cgmPath, loadOptions))
            {
                // Save the loaded content as a PDF file
                doc.Save(pdfPath);
            }

            Console.WriteLine($"CGM successfully converted to PDF: {pdfPath}");
        }
        // Aspose.Pdf throws PdfException for PDF‑related errors
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        // Catch any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}