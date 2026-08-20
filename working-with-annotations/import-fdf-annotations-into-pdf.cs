using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "data.fdf";
        const string outputPath = "merged_output.pdf";

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
            // Load the existing PDF document.
            using (Document doc = new Document(pdfPath))
            // Open the FDF file as a stream.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations (including form field data) from the FDF stream into the PDF.
                FdfReader.ReadAnnotations(fdfStream, doc);

                // Save the updated PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}