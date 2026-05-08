using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class XmlToPdfFieldPopulator
{
    static void Main()
    {
        // Paths to required files
        const string pdfTemplatePath = "template.pdf";      // PDF with form fields
        const string xmlDataPath     = "data.xml";         // Source XML
        const string xsltPath        = "transform.xslt";   // XSLT that produces XFDF
        const string outputPdfPath   = "filled_output.pdf";

        // Verify files exist
        if (!File.Exists(pdfTemplatePath) ||
            !File.Exists(xmlDataPath) ||
            !File.Exists(xsltPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Transform the XML using the XSLT stylesheet to produce XFDF.
        //    The XSLT is expected to output an XFDF document that contains
        //    field names and values matching the PDF form.
        // -----------------------------------------------------------------
        MemoryStream xfdfStream = new MemoryStream();
        try
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (XmlReader xmlReader = XmlReader.Create(xmlDataPath))
            using (XmlWriter xfdfWriter = XmlWriter.Create(xfdfStream, new XmlWriterSettings { Encoding = System.Text.Encoding.UTF8, CloseOutput = false }))
            {
                xslt.Transform(xmlReader, xfdfWriter);
            }

            // Reset stream position for reading
            xfdfStream.Position = 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"XSLT transformation failed: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Load the PDF form template.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // -----------------------------------------------------------------
            // 3. Import field values from the generated XFDF into the PDF.
            //    XfdfReader.ReadFields reads the XFDF stream and populates matching
            //    form fields in the document.
            // -----------------------------------------------------------------
            try
            {
                XfdfReader.ReadFields(xfdfStream, pdfDoc);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to import XFDF fields: {ex.Message}");
                return;
            }

            // -----------------------------------------------------------------
            // 4. (Optional) Flatten the form if you want the fields to become
            //    regular content. Comment out if interactive fields are required.
            // -----------------------------------------------------------------
            // pdfDoc.Flatten();

            // -----------------------------------------------------------------
            // 5. Save the resulting PDF.
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with populated fields saved to '{outputPdfPath}'.");
    }
}