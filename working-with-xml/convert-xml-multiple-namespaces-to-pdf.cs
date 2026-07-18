using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
// No additional namespaces are required for the load options

class XmlToPdfWithMultipleNamespaces
{
    static void Main()
    {
        // Paths to the source XML, the XSLT stylesheet and the output PDF.
        const string xmlFile   = @"C:\Data\source.xml";
        const string xslFile   = @"C:\Data\transform.xslt";
        const string pdfFile   = @"C:\Data\output.pdf";

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

        // --------------------------------------------------------------------
        // Load the XML document and apply the XSLT transformation.
        // XmlLoadOptions accepts the XSLT file path (or a stream) and
        // internally resolves all namespace prefixes defined in the stylesheet.
        // By providing the stylesheet via the constructor we avoid any
        // ambiguity that could arise from multiple namespaces in the XML.
        // --------------------------------------------------------------------
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFile);

        // Use a using block to ensure the Document is disposed properly.
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions are needed because the
            // target format is PDF (the default).
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"PDF successfully created at: {pdfFile}");
    }
}