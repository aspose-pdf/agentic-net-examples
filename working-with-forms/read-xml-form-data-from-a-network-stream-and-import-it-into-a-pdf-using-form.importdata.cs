using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // XfdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // existing PDF with form fields
        const string outputPdfPath   = "filled.pdf";    // result PDF
        const string xfdfUrl         = "https://example.com/formdata.xfdf"; // XML form data source

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Retrieve the XFDF data from the network as a stream
                using (HttpClient httpClient = new HttpClient())
                using (Stream xfdfStream = httpClient.GetStreamAsync(xfdfUrl).Result)
                {
                    // Import field values from the XFDF (XML) stream into the PDF form
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // Save the updated PDF with imported form data
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}