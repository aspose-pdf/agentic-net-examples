using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSLT stylesheet and the output PDF.
        const string xmlPath = "input.xml";
        const string xslPath = "transform.xsl";
        const string pdfPath = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath) || !File.Exists(xslPath))
        {
            Console.Error.WriteLine("XML or XSLT file not found.");
            return;
        }

        // Load the XSLT stylesheet as a stream.
        // Passing the XSLT stream to XmlLoadOptions ensures that the
        // transformation uses the exact namespaces declared in the stylesheet,
        // avoiding conflicts that can arise when the XSLT is loaded by name.
        using (FileStream xslStream = File.OpenRead(xslPath))
        {
            // XmlLoadOptions can be constructed with an XSLT stream.
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslStream);

            // Create the PDF document by applying the XSLT to the XML.
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // Save the resulting PDF.
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}