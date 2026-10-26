using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for XmlLoadOptions/XmlSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string intermediateXml   = "intermediate.xml";
        const string transformedXml    = "transformed.xml";
        const string xsltPath          = "addPageNumbers.xslt";
        const string outputPdfPath     = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // 1. Load the original PDF.
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // 2. Save the PDF to XML (non‑PDF format requires explicit XmlSaveOptions).
            srcDoc.Save(intermediateXml, new XmlSaveOptions());
        }

        // 3. Apply XSLT that inserts page numbers into the XML representation.
        //    The XSLT should add a <PageNumber> element or similar at the desired location.
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);
        using (XmlReader reader = XmlReader.Create(intermediateXml))
        using (XmlWriter writer = XmlWriter.Create(transformedXml, xslt.OutputSettings))
        {
            xslt.Transform(reader, writer);
        }

        // 4. Load the transformed XML back into a PDF document.
        using (Document finalDoc = new Document(transformedXml, new XmlLoadOptions()))
        {
            // 5. Save the final PDF with page numbers.
            finalDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with automatic page numbers saved to '{outputPdfPath}'.");
    }
}