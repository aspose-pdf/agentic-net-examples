using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file that contains references to external CSS via an XSL stylesheet.
        const string xmlPath = "input.xml";

        // XSL stylesheet that transforms the XML into PDF content.
        // The stylesheet can include <link rel="stylesheet" href="style.css"/> or similar mechanisms.
        const string xslPath = "transform.xsl";

        // Output PDF file.
        const string pdfPath = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL file not found: {xslPath}");
            return;
        }

        try
        {
            // Create a new PDF document instance.
            using (Document pdfDocument = new Document())
            {
                // Bind the XML source together with the XSL stylesheet.
                // This causes Aspose.Pdf to apply the transformation, including any external CSS
                // referenced by the XSL, when generating the PDF content.
                pdfDocument.BindXml(xmlPath, xslPath);

                // Save the resulting PDF.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"PDF generated successfully: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF generation: {ex.Message}");
        }
    }
}