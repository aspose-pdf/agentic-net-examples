using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // PDF with form fields
        const string fdfPath = "data.fdf";    // FDF/XFDF file containing field values
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Import field values from the FDF/XFDF stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // For XFDF use XfdfReader.ReadFields; it also works for FDF-like streams
                XfdfReader.ReadFields(fdfStream, doc);
            }

            // Flatten the form to make fields non‑editable
            doc.Form.Flatten();

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and flattened. Saved to '{outputPath}'.");
    }
}