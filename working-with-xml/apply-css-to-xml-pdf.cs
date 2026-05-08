using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ApplyCssToXmlPdf
{
    static void Main()
    {
        // Paths to the source XML, external CSS and the output PDF.
        const string xmlPath   = "source.xml";
        const string cssPath   = "style.css";
        const string pdfPath   = "styled_output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(cssPath))
        {
            Console.Error.WriteLine($"CSS file not found: {cssPath}");
            return;
        }

        try
        {
            // Load the XML document. No XSL is supplied here because the CSS
            // will be applied via the BindXml overload that accepts an XSL stream.
            // The CSS file is treated as an XSL stylesheet that contains the
            // necessary transformation rules (Aspose.Pdf can process CSS‑based
            // XSLT when converting XML to PDF).
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            using (FileStream cssStream = File.OpenRead(cssPath))
            {
                // Create a new empty PDF document.
                using (Document pdfDoc = new Document())
                {
                    // Bind the XML and the CSS (as XSL) to the document.
                    // This overload attaches the XML content and applies the
                    // stylesheet, resulting in a styled PDF.
                    pdfDoc.BindXml(xmlStream, cssStream);

                    // Save the resulting PDF.
                    pdfDoc.Save(pdfPath);
                }
            }

            Console.WriteLine($"PDF generated successfully: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}