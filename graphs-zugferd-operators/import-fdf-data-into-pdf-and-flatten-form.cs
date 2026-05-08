using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // PDF with form fields
        const string fdfPath   = "data.fdf";    // FDF containing field values
        const string outputPath = "filled_flattened.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath) || !File.Exists(fdfPath))
        {
            Console.Error.WriteLine("Input PDF or FDF file not found.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Import field values (and any associated annotations) from the FDF stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Flatten the form so that field values become part of the page content
            doc.Form.Flatten();

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported data saved to '{outputPath}'.");
    }
}