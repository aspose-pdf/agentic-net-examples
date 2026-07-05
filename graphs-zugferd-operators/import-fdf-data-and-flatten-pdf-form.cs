using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // PDF with form fields
        const string fdfPath   = "data.fdf";    // FDF/XFDF file containing field values
        const string outputPath = "output.pdf"; // Resulting flattened PDF

        // Verify input files exist
        if (!File.Exists(pdfPath) || !File.Exists(fdfPath))
        {
            Console.Error.WriteLine("Input PDF or FDF file not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF/XFDF stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import field values from the XFDF/FDF stream into the document
                // XfdfReader.ReadFields works for XFDF; if the file is plain FDF,
                // Aspose.Pdf also accepts it via the same method.
                XfdfReader.ReadFields(fdfStream, doc);
            }

            // Flatten the form so fields become part of the page content and cannot be edited
            doc.Form.Flatten();

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and PDF flattened: '{outputPath}'.");
    }
}