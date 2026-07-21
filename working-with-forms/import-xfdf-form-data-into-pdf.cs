using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // PDF with form fields
        const string xfdfPath  = "data.xfdf";      // XML (XFDF) containing field values
        const string outputPdf = "output.pdf";     // Resulting PDF with populated fields

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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the XFDF (XML) stream and import field values
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // Save the updated PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Form data imported successfully. Saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}