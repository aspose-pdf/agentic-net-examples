using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string xsltPath      = "addPageNumbers.xslt";
        const string outputPdfPath = "output_paginated.pdf";

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

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // 1. Export PDF to XML (in-memory)
            using (MemoryStream xmlStream = new MemoryStream())
            {
                pdfDoc.Save(xmlStream, SaveFormat.Xml);
                xmlStream.Position = 0; // rewind for reading

                // 2. Apply XSLT that inserts page numbers
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                using (MemoryStream transformedXml = new MemoryStream())
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                using (XmlWriter xmlWriter = XmlWriter.Create(transformedXml))
                {
                    xslt.Transform(xmlReader, xmlWriter);
                    xmlWriter.Flush();
                    transformedXml.Position = 0; // rewind for loading

                    // 3. Load the transformed XML back into a PDF document
                    XmlLoadOptions loadOpts = new XmlLoadOptions();
                    using (Document resultDoc = new Document(transformedXml, loadOpts))
                    {
                        // 4. Save the final PDF with page numbers
                        resultDoc.Save(outputPdfPath);
                    }
                }
            }
        }

        Console.WriteLine($"Paginated PDF saved to '{outputPdfPath}'.");
    }
}