using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source files
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string xmlDataPath     = "data.xml";       // Source XML
        const string xsltPath        = "transform.xslt"; // XSLT that produces XFDF
        const string outputPdfPath   = "filled_output.pdf";

        // Verify that all required files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Load the PDF form template
            // -----------------------------------------------------------------
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // -----------------------------------------------------------------
                // 2. Transform the XML using the provided XSLT.
                //    The XSLT is expected to generate XFDF (field name/value pairs).
                // -----------------------------------------------------------------
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    // Prepare the XSLT processor
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(xsltPath);

                    // Perform the transformation: XML -> XFDF (written to memory stream)
                    using (XmlReader xmlReader = XmlReader.Create(xmlDataPath))
                    using (XmlWriter xmlWriter = XmlWriter.Create(xfdfStream, xslt.OutputSettings))
                    {
                        xslt.Transform(xmlReader, xmlWriter);
                    }

                    // Reset stream position for reading
                    xfdfStream.Position = 0;

                    // -----------------------------------------------------------------
                    // 3. Import the XFDF field values into the PDF document.
                    //    XfdfReader.ReadFields reads the field data and populates the form.
                    // -----------------------------------------------------------------
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // -----------------------------------------------------------------
                // 4. Save the populated PDF.
                // -----------------------------------------------------------------
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}