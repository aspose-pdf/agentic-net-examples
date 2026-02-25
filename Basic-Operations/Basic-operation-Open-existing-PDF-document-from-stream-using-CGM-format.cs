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
            // Open the CGM file as a stream and load it with CGM load options
            using (FileStream stream = File.OpenRead(cgmPath))
            {
                CgmLoadOptions loadOptions = new CgmLoadOptions();

                // Create a Document from the CGM stream
                using (Document doc = new Document(stream, loadOptions))
                {
                    // Save the resulting PDF
                    doc.Save(pdfPath);
                }
            }

            Console.WriteLine($"CGM converted to PDF: '{pdfPath}'");
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