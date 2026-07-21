using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output XML
        const string inputPdfPath  = "TaxForm.pdf";
        const string outputXmlPath = "TaxFormData.xml";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Retrieve the tax field by its name and cast to Aspose.Pdf.Forms.Field
            Field taxField = pdfDoc.Form["Tax"] as Field;
            if (taxField == null)
            {
                Console.Error.WriteLine("Tax field not found or is not a form field.");
                return;
            }

            // Example calculation: set the tax value (replace with real calculation logic)
            taxField.Value = "123.45";

            // Export the entire form data to XML using XmlSaveOptions
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            pdfDoc.Save(outputXmlPath, xmlOptions);
        }

        Console.WriteLine($"Form data exported to XML: {outputXmlPath}");
    }
}