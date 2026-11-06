using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string fdfPath    = "data.fdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Use the Form facade to import FDF data
                using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(pdfDoc))
                {
                    using (FileStream fdfStream = File.OpenRead(fdfPath))
                    {
                        // Import form data from the FDF stream (ImportFdf, not ImportDataFromFDF)
                        pdfForm.ImportFdf(fdfStream);
                    }
                }

                // Save the updated PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
