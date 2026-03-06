using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XFDF file with field values, and the output PDF
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "data.xfdf";
        const string outputPath = "output.pdf";

        // Verify that the input files exist
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

        // Load the PDF document (AcroForm fields are accessible via Document.Form)
        using (Document doc = new Document(pdfPath))
        {
            // Open the XFDF stream and import field values into the document
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                XfdfReader.ReadFields(xfdfStream, doc);
            }

            // Save the updated PDF with the imported field values
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported XFDF fields saved to '{outputPath}'.");
    }
}