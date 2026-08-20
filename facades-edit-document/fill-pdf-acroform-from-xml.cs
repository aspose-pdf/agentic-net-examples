using System;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // needed for the Form facade
using Aspose.Pdf.Forms;            // needed for TextBoxField and other field types

class Program
{
    static void Main()
    {
        // Paths for the temporary files used in this self‑contained example.
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath    = "filled.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF that contains the required AcroForm fields.
        // ---------------------------------------------------------------------
        using (Document seedDoc = new Document())
        {
            // Add a page.
            Page page = seedDoc.Pages.Add();

            // Create a text box for "FirstName".
            TextBoxField firstNameField = new TextBoxField(page, new Rectangle(100, 700, 200, 720))
            {
                PartialName = "FirstName",
                Value = string.Empty
            };
            seedDoc.Form.Add(firstNameField, 1);

            // Create a text box for "LastName".
            TextBoxField lastNameField = new TextBoxField(page, new Rectangle(100, 650, 200, 670))
            {
                PartialName = "LastName",
                Value = string.Empty
            };
            seedDoc.Form.Add(lastNameField, 1);

            // Save the template PDF that will later be filled.
            seedDoc.Save(pdfTemplatePath);
        }

        // ---------------------------------------------------------------------
        // 2. Prepare the XML data (in‑memory – no external file required).
        // ---------------------------------------------------------------------
        string xmlContent = @"<Root><FirstName>John</FirstName><LastName>Doe</LastName></Root>";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        // Helper function to extract a value using an XPath expression.
        string GetValue(string xpath)
        {
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            return node?.InnerText ?? string.Empty;
        }

        // Extract the values for the form fields.
        string firstName = GetValue("//FirstName");
        string lastName  = GetValue("//LastName");

        // ---------------------------------------------------------------------
        // 3. Fill the AcroForm fields using the Form facade (new API).
        // ---------------------------------------------------------------------
        // NOTE: Both Aspose.Pdf.Facades and Aspose.Pdf.Forms contain a type named "Form".
        // To avoid the CS0104 ambiguous‑reference error we fully qualify the facade class.
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfTemplatePath))
        {
            // Populate the fields.
            form.FillField("FirstName", firstName);
            form.FillField("LastName",  lastName);

            // Save the filled PDF to the desired output path.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields have been populated and saved to '{outputPdfPath}'.");
    }
}
