using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDF template, source XML, XSLT stylesheet and output PDF
        const string templatePath = "template.pdf";
        const string xmlPath      = "data.xml";
        const string xslPath      = "transform.xsl";
        const string outputPath   = "filled.pdf";

        // Verify that all required files exist
        if (!File.Exists(templatePath) || !File.Exists(xmlPath) || !File.Exists(xslPath))
        {
            Console.Error.WriteLine("One or more input files were not found.");
            return;
        }

        try
        {
            // Load the PDF that contains form fields
            using (Document pdfDoc = new Document(templatePath))
            {
                // Bind the XML data to the PDF using the XSLT stylesheet.
                // This populates the form fields according to the XSLT mapping.
                pdfDoc.BindXml(xmlPath, xslPath);

                // Save the populated PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}