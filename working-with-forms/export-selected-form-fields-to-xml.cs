using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // PDF containing the form
        const string outputXml = "selected_fields.xml";    // XML file to receive the export

        // Specify the exact field names that should be exported.
        // Use the full field names as they appear in the PDF form.
        string[] fieldsToExport = new string[]
        {
            "FirstName",
            "LastName",
            "Email"
        };

        // Ensure the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Form facade to bind the PDF and export the whole form to XML.
        // Afterwards, keep only the requested fields.
        using (Form form = new Form())
        {
            // Bind the PDF document to the Form facade.
            form.BindPdf(inputPdf);

            // Export the complete form data to a memory stream.
            using (MemoryStream fullXmlStream = new MemoryStream())
            {
                form.ExportXml(fullXmlStream);
                fullXmlStream.Position = 0; // rewind for reading

                // Load the XML into XDocument for manipulation.
                XDocument doc = XDocument.Load(fullXmlStream);

                // The exported XML typically has a root element <formdata> and child
                // elements <field name="...">value</field>. Adjust the query if the
                // structure differs for your version of Aspose.PDF.
                var fields = doc.Root?.Elements("field").ToList();
                if (fields != null)
                {
                    foreach (var field in fields)
                    {
                        XAttribute nameAttr = field.Attribute("name");
                        if (nameAttr == null || !fieldsToExport.Contains(nameAttr.Value))
                        {
                            field.Remove(); // drop fields not requested
                        }
                    }
                }

                // Save the filtered XML to the target file.
                using (FileStream xmlFile = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                {
                    doc.Save(xmlFile);
                }
            }
        }

        Console.WriteLine($"Selected form fields exported to '{outputXml}'.");
    }
}
