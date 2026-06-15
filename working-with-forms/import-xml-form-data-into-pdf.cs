using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Entry point
    static async Task Main()
    {
        const string pdfTemplatePath = "template.pdf";      // PDF with form fields
        const string outputPdfPath   = "filled_form.pdf";   // Resulting PDF
        const string xmlDataUrl      = "https://example.com/formdata.xml"; // URL returning XML

        // Verify the template PDF exists
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        // Download the XML form data as a stream
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                using (Stream xmlStream = await httpClient.GetStreamAsync(xmlDataUrl))
                {
                    // Load the PDF document that contains the interactive form
                    using (Document pdfDoc = new Document(pdfTemplatePath))
                    {
                        // Import the XML data into the PDF.
                        // BindXml reads the XML and populates the form fields (XFA or AcroForm).
                        pdfDoc.BindXml(xmlStream);

                        // Save the updated PDF
                        pdfDoc.Save(outputPdfPath);
                    }

                    Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Failed to download XML data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
            }
        }
    }
}