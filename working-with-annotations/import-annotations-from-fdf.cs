using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "Resources/annotations.fdf";
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
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Open the FDF file stream
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // Import annotations from the FDF into the PDF document
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }

                // Save the updated PDF with imported annotations
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with imported annotations saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}