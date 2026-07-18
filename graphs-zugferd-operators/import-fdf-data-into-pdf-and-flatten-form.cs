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
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF file stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import form field values (and any annotations) from the FDF file
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Flatten the form so that field values become part of the page content and cannot be edited
            doc.Flatten();

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and flattened. Saved to '{outputPath}'.");
    }
}