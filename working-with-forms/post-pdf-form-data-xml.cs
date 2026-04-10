using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Entry point
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to the PDF with form fields
        const string endpointUrl = "https://example.com/submit"; // Remote XML endpoint

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Ensure the document contains a form
                if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Build an XML document containing form field names and values
                XDocument xmlDoc = new XDocument(
                    new XElement("FormData",
                        // Iterate over each field and add an element with its name and value
                        BuildFieldElements(pdfDoc.Form)
                    )
                );

                // Convert the XML to a string for posting
                string xmlString = xmlDoc.Declaration + xmlDoc.ToString();

                // Post the XML to the remote endpoint
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(xmlString, Encoding.UTF8, "application/xml");
                    HttpResponseMessage response = client.PostAsync(endpointUrl, content).GetAwaiter().GetResult();

                    Console.WriteLine($"POST status: {(int)response.StatusCode} {response.ReasonPhrase}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to create XML elements for each form field
    private static object[] BuildFieldElements(Form form)
    {
        var elements = new object[form.Count];
        int index = 0;

        foreach (Field field in form)
        {
            // Use the field's partial name as the element name and its value as the content
            string fieldName = field.PartialName ?? $"Field{index}";
            string fieldValue = field.Value?.ToString() ?? string.Empty;

            elements[index++] = new XElement(fieldName, fieldValue);
        }

        return elements;
    }
}