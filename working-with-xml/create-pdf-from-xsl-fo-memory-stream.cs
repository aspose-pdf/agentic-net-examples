using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Sample XML content to be converted
        string xmlContent = "<root><message>Hello, Aspose.Pdf!</message></root>";

        // Wrap the XML inside a minimal XSL‑FO document (Aspose.Pdf can only load XSL‑FO)
        string xslFo = $@"<?xml version='1.0' encoding='UTF-8'?>
<fo:root xmlns:fo='http://www.w3.org/1999/XSL/Format'>
  <fo:layout-master-set>
    <fo:simple-page-master master-name='simple' page-height='29.7cm' page-width='21cm' margin='2cm'>
      <fo:region-body/>
    </fo:simple-page-master>
  </fo:layout-master-set>
  <fo:page-sequence master-reference='simple'>
    <fo:flow flow-name='xsl-region-body'>
      <fo:block>{xmlContent}</fo:block>
    </fo:flow>
  </fo:page-sequence>
</fo:root>";

        // Convert the XSL‑FO string to a memory stream (UTF‑8 encoding)
        using (MemoryStream foStream = new MemoryStream(Encoding.UTF8.GetBytes(xslFo)))
        {
            // Load options for XSL‑FO (no XSLT transformation required)
            XslFoLoadOptions foLoadOptions = new XslFoLoadOptions();

            // Load the XSL‑FO into a PDF document using the Document constructor
            using (Document pdfDocument = new Document(foStream, foLoadOptions))
            {
                // Save the resulting PDF with default settings
                pdfDocument.Save("output.pdf");
            }
        }

        Console.WriteLine("PDF document created successfully.");
    }
}
