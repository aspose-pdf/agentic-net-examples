using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";      // Original PDF with form fields
        const string xfdfPath     = "transformed.xfdf"; // XML (XFDF) containing updated field values
        const string outputPdfPath = "output.pdf";     // PDF after fields are repopulated

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
            // Load the original PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the XFDF (XML) stream containing the transformed field data
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // Import field values from the XFDF stream into the PDF document
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // Save the updated PDF with repopulated fields
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Fields imported successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}