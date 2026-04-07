using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // For advanced conversion if needed

class XmlToPdfWithNamespaces
{
    static void Main()
    {
        // Paths to the source XML, the XSLT that performs the transformation,
        // and the desired PDF output.
        const string xmlFile   = @"C:\Data\source.xml";
        const string xslFile   = @"C:\Data\transform.xslt";
        const string pdfOutput = @"C:\Data\result.pdf";

        // Verify that the input files exist.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }
        if (!File.Exists(xslFile))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslFile}");
            return;
        }

        // -----------------------------------------------------------------
        // Load the XML file and apply the XSLT while creating the PDF.
        // XmlLoadOptions can be constructed with the XSLT file path.
        // The XSLT itself should declare all required namespaces with
        // distinct prefixes (e.g., xmlns:ns1="http://example.com/ns1",
        // xmlns:ns2="http://example.com/ns2") to avoid name collisions.
        // -----------------------------------------------------------------
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFile);

        // Use a using block for deterministic disposal of the Document object.
        using (Document pdfDoc = new Document(xmlFile, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions are needed because the
            // target format is PDF.
            pdfDoc.Save(pdfOutput);
        }

        Console.WriteLine($"PDF successfully created at: {pdfOutput}");
    }
}