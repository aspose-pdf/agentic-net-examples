using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF containing the tax calculation form.
        const string inputPdfPath = "tax_form.pdf";

        // Output XML file that will hold the exported form data.
        const string outputXmlPath = "tax_data.xml";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // At this point the form has been submitted and the calculated
            // tax value is stored in the form fields. Export the entire PDF
            // (including form field values) to XML using XmlSaveOptions.
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document as XML. The file will contain the form field
            // values, which downstream processes can parse.
            pdfDocument.Save(outputXmlPath, xmlOptions);
        }

        Console.WriteLine($"Form data exported to XML: {outputXmlPath}");
    }
}