using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // PDF with form fields
        const string fdfPath = "data.fdf";    // FDF containing field values
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath) || !File.Exists(fdfPath))
        {
            Console.Error.WriteLine("Input PDF or FDF file not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Import field values (and any annotations) from the FDF stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Flatten the form so fields become static content and cannot be edited
            doc.Form.Flatten();

            // Save the modified PDF (lifecycle rule: use Save without extra options)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and flattened. Saved to '{outputPath}'.");
    }
}