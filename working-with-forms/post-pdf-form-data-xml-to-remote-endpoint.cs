using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing a form.
        const string pdfPath = "input.pdf";

        // Remote XML endpoint that will receive the posted form data.
        const string endpointUrl = "https://example.com/receive";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Build a simple XML representation of the form fields.
            XDocument xmlDoc = new XDocument(new XElement("FormData"));

            foreach (Field field in doc.Form.Fields)
            {
                string fieldName = field.PartialName ?? string.Empty;
                string fieldValue = string.Empty;

                switch (field)
                {
                    case TextBoxField txt:
                        fieldValue = txt.Value ?? string.Empty;
                        break;
                    case CheckboxField chk: // Correct class name (lowercase 'b')
                        fieldValue = chk.Checked ? "true" : "false";
                        break;
                    case RadioButtonField rad:
                        fieldValue = rad.Value ?? string.Empty;
                        break;
                    case ListBoxField list:
                        // SelectedItems returns an int[] of selected indices.
                        if (list.SelectedItems != null && list.SelectedItems.Length > 0)
                        {
                            fieldValue = string.Join(",", list.SelectedItems.Select(i => i.ToString()));
                        }
                        else
                        {
                            fieldValue = string.Empty;
                        }
                        break;
                    default:
                        // Fallback for any other field types that expose a Value property.
                        if (field.Value != null)
                            fieldValue = field.Value.ToString();
                        break;
                }

                xmlDoc.Root.Add(new XElement("Field",
                    new XAttribute("name", fieldName),
                    new XAttribute("type", field.GetType().Name),
                    new XText(fieldValue)));
            }

            // Export the XML to an in‑memory stream.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlDoc.Save(xmlStream);
                xmlStream.Position = 0; // Reset stream position before reading.

                // Post the XML data to the remote endpoint using HttpClient.
                using (HttpClient client = new HttpClient())
                using (StreamContent content = new StreamContent(xmlStream))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                    HttpResponseMessage response = client.PostAsync(endpointUrl, content)
                                                          .GetAwaiter()
                                                          .GetResult();
                    Console.WriteLine($"Form data posted successfully. Status: {response.StatusCode}");
                }
            }
        }
    }
}
