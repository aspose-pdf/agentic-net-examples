using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document containing the data
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Retrieve values using XPath – adjust the expressions to match your XML schema
        XmlNode nameNode = xmlDoc.SelectSingleNode("//Customer/Name");
        XmlNode emailNode = xmlDoc.SelectSingleNode("//Customer/Email");
        XmlNode agreeNode = xmlDoc.SelectSingleNode("//Customer/AgreeTerms");

        // Open the PDF form and fill fields
        using (Form pdfForm = new Form(pdfPath))
        {
            if (nameNode != null)
            {
                pdfForm.FillField("Customer.Name", nameNode.InnerText);
            }

            if (emailNode != null)
            {
                pdfForm.FillField("Customer.Email", emailNode.InnerText);
            }

            if (agreeNode != null)
            {
                bool agree = string.Equals(agreeNode.InnerText, "true", StringComparison.OrdinalIgnoreCase);
                pdfForm.FillField("Customer.AgreeTerms", agree);
            }

            // Save the filled PDF
            pdfForm.Save(outputPath);
        }

        Console.WriteLine($"Form fields filled and saved to '{outputPath}'.");
    }
}