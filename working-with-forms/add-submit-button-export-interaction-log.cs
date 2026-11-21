using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlLogPath = "interaction_log.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF containing the submitted form.
            using (Document doc = new Document(pdfPath))
            {
                var form = doc.Form;
                if (form == null || form.Fields == null || form.Fields.Count() == 0)
                {
                    Console.WriteLine("No form fields found to export.");
                }
                else
                {
                    // Build an XML document that contains each field name and its value.
                    XDocument xDoc = new XDocument(new XElement("FormData"));
                    foreach (Field field in form.Fields)
                    {
                        string name = field.Name ?? field.FullName ?? "Unnamed";
                        string value = field.Value?.ToString() ?? string.Empty;
                        XElement fieldElem = new XElement("Field",
                            new XAttribute("Name", name),
                            new XAttribute("Value", value));
                        xDoc.Root.Add(fieldElem);
                    }

                    // Save the XML log.
                    xDoc.Save(xmlLogPath);
                    Console.WriteLine($"Interaction log exported to '{xmlLogPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
