using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a working directory inside the current folder.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        Directory.CreateDirectory(dataDir);

        // --------------------------------------------------------------------
        // 0. Prepare sample files so the example can run in an empty sandbox.
        // --------------------------------------------------------------------
        // 0.1. Simple XSL‑FO template (static content).
        string xslFoTemplatePath = Path.Combine(dataDir, "template.xslfo");
        File.WriteAllText(xslFoTemplatePath,
@"<?xml version='1.0' encoding='UTF-8'?>
<fo:root xmlns:fo='http://www.w3.org/1999/XSL/Format'>
  <fo:layout-master-set>
    <fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'>
      <fo:region-body/>
    </fo:simple-page-master>
  </fo:layout-master-set>
  <fo:page-sequence master-reference='A4'>
    <fo:flow flow-name='xsl-region-body'>
      <fo:block font-size='14pt' text-align='center'>Static XSL‑FO Template</fo:block>
    </fo:flow>
  </fo:page-sequence>
</fo:root>");

        // 0.2. Sample XML data.
        string xmlPath = Path.Combine(dataDir, "data.xml");
        File.WriteAllText(xmlPath,
@"<?xml version='1.0' encoding='UTF-8'?>
<root>
  <message>Hello from XML!</message>
</root>");

        // 0.3. XSL‑FO stylesheet (XSLT that produces FO from the XML above).
        string xslFoStylesheetPath = Path.Combine(dataDir, "stylesheet.xslfo");
        File.WriteAllText(xslFoStylesheetPath,
@"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:fo='http://www.w3.org/1999/XSL/Format'>
  <xsl:output method='xml' indent='yes'/>
  <xsl:template match='/'>
    <fo:root>
      <fo:layout-master-set>
        <fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'>
          <fo:region-body/>
        </fo:simple-page-master>
      </fo:layout-master-set>
      <fo:page-sequence master-reference='A4'>
        <fo:flow flow-name='xsl-region-body'>
          <fo:block font-size='12pt' text-align='left'>
            <xsl:value-of select='root/message'/>
          </fo:block>
        </fo:flow>
      </fo:page-sequence>
    </fo:root>
  </xsl:template>
</xsl:stylesheet>");

        // --------------------------------------------------------------------
        // 1. Convert a ready XSL‑FO file directly to PDF.
        // --------------------------------------------------------------------
        string pdfFromFo = Path.Combine(dataDir, "result_from_fo.pdf");
        XslFoLoadOptions foLoadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoTemplatePath, foLoadOptions))
        {
            pdfDoc.Save(pdfFromFo);
        }

        // --------------------------------------------------------------------
        // 2. Convert XML data using an external XSL‑FO stylesheet.
        // --------------------------------------------------------------------
        string pdfFromXml = Path.Combine(dataDir, "result_from_xml.pdf");
        using (Document doc = new Document())
        {
            // BindXml applies the XSL‑FO transformation to the XML and builds the PDF.
            doc.BindXml(xmlPath, xslFoStylesheetPath);
            doc.Save(pdfFromXml);
        }

        Console.WriteLine("PDF generation completed. Files are located in: " + dataDir);
    }
}
