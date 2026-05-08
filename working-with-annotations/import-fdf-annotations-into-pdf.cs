using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Needed for FdfReader

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // Existing PDF with form fields
        const string fdfPath = "data.fdf";       // FDF file containing form data/annotations
        const string outputPath = "merged.pdf";  // Resulting PDF after import

        // Verify input files exist
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
            // Load the existing PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the FDF stream and import its annotations into the PDF
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                }

                // Save the updated PDF with the imported form data
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}