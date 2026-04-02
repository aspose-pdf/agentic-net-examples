using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare sample XML content
        string xmlPath = "content.xml";
        string xmlContent = "<?xml version='1.0' encoding='UTF-8'?><document><title>Sample Document</title><section><para>This is a sample paragraph that will be repeated on each page.</para></section></document>";
        File.WriteAllText(xmlPath, xmlContent);

        // Prepare XSL-FO stylesheet that adds header/footer page numbers
        string xslPath = "template.xsl";
        string xslContent = "<?xml version='1.0' encoding='UTF-8'?><xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform' xmlns:fo='http://www.w3.org/1999/XSL/Format'>" +
                           "<xsl:output method='xml' indent='yes'/>" +
                           "<xsl:template match='/'><fo:root><fo:layout-master-set><fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'><fo:region-body/><fo:region-before extent='1cm'/><fo:region-after extent='1cm'/></fo:simple-page-master></fo:layout-master-set><fo:page-sequence master-reference='A4'><fo:static-content flow-name='xsl-region-before'><fo:block text-align='center' font-size='12pt'>Header - Page <fo:page-number/></fo:block></fo:static-content><fo:static-content flow-name='xsl-region-after'><fo:block text-align='center' font-size='12pt'>Footer - Page <fo:page-number/></fo:block></fo:static-content><fo:flow flow-name='xsl-region-body'><xsl:apply-templates select='document'/></fo:flow></fo:page-sequence></fo:root></xsl:template>" +
                           "<xsl:template match='document'><fo:block font-size='14pt' font-weight='bold' text-align='center' margin-bottom='12pt'><xsl:value-of select='title'/></fo:block><xsl:apply-templates select='section/para'/></xsl:template>" +
                           "<xsl:template match='para'><fo:block margin-bottom='6pt'><xsl:value-of select='.'/></fo:block></xsl:template>" +
                           "</xsl:stylesheet>";
        File.WriteAllText(xslPath, xslContent);

        // Create a new PDF document and bind the XML with the XSL-FO stylesheet
        using (Aspose.Pdf.Document document = new Aspose.Pdf.Document())
        {
            document.BindXml(xmlPath, xslPath);
            document.Save("output.pdf");
        }

        // Optional cleanup of temporary files
        File.Delete(xmlPath);
        File.Delete(xslPath);
    }
}
