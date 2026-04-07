using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XFDF (XML) data file, and the output PDF
        const string pdfPath = "input.pdf";
        const string xfdfPath = "data.xfdf";
        const string outputPath = "output.pdf";

        // Verify that the required files exist
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
            // Load the PDF document (wrapped in a using block for deterministic disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the XFDF file as a stream
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // Import field values from the XFDF (XML) stream into the PDF form
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // Save the updated PDF with the imported form data
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}