using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // Source PDF with form fields
        const string xfdfPath  = "data.xfdf";   // XFDF (XML) file containing field values
        const string outputPath = "output.pdf"; // Resulting PDF after import

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Open the XFDF stream and import field values
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                // Reads field values from the XFDF stream and updates matching fields in the PDF
                XfdfReader.ReadFields(xfdfStream, pdfDoc);
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported successfully. Saved to '{outputPath}'.");
    }
}