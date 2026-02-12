using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input CGM file and output PDF file paths
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string cgmPath = Path.Combine(dataDir, "sample.cgm");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // Create load options for CGM conversion (default A4 page size)
            CgmLoadOptions loadOptions = new CgmLoadOptions();

            // Load the CGM file into a PDF Document using the load options
            using (Document pdfDocument = new Document(cgmPath, loadOptions))
            {
                // Save the resulting PDF document
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"CGM file successfully converted and saved to: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}