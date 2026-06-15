using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // Source PDF with form fields
        const string xfdfPath = "data.xfdf";     // XML (XFDF) file containing field values
        const string outputPath = "output.pdf";  // Resulting PDF after import

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the XFDF (XML) stream
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // Import matching form field values from the XFDF stream into the PDF
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // Save the updated PDF (lifecycle rule: use Save without extra options)
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}