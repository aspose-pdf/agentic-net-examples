using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // XfdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // existing PDF with form fields
        const string xfdfPath  = "data.xfdf";      // XML (XFDF) containing a subset of field values
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Open the XFDF (XML) stream and import matching field values
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                // XfdfReader.ReadFields imports only the fields that exist in both the XFDF and the PDF
                XfdfReader.ReadFields(xfdfStream, doc);
            }

            // Optionally flatten the form so that fields become static content
            // doc.Flatten(); // uncomment if you want to remove interactive fields

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF updated and saved to '{outputPdf}'.");
    }
}