using System;
using System.IO;
using Aspose.Pdf;

class ApplyCssFromXml
{
    static void Main()
    {
        // Paths to the source XML, the XSL stylesheet (which can reference external CSS),
        // and the desired PDF output.
        const string xmlFile   = "source.xml";
        const string xslFile   = "style.xsl";
        const string pdfOutput = "styled_output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }
        if (!File.Exists(xslFile))
        {
            Console.Error.WriteLine($"XSL file not found: {xslFile}");
            return;
        }

        try
        {
            // Load the XSL stylesheet into a stream. The XSL can contain processing
            // instructions that reference external CSS files; Aspose.Pdf will apply those
            // styles during the XML‑to‑PDF conversion.
            using (FileStream xslStream = File.OpenRead(xslFile))
            {
                // Create XmlLoadOptions with the XSL stream.
                XmlLoadOptions loadOptions = new XmlLoadOptions(xslStream);

                // Load the XML document together with the XSL options.
                using (Document pdfDoc = new Document(xmlFile, loadOptions))
                {
                    // Save the resulting PDF. All CSS rules referenced by the XSL
                    // (and any linked CSS files) are applied to the PDF content.
                    pdfDoc.Save(pdfOutput);
                }
            }

            Console.WriteLine($"PDF generated successfully: {pdfOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}