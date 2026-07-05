using System;
using System.IO;
using System.Net;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";
        const string xfdfUrl = "https://example.com/formdata.xfdf";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfTemplatePath))
            {
                // Retrieve XFDF (XML) data from the network
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xfdfUrl);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream xfdfStream = response.GetResponseStream())
                {
                    // Import field values from the XFDF stream into the PDF form
                    XfdfReader.ReadFields(xfdfStream, doc);
                }

                // Save the updated PDF
                doc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}