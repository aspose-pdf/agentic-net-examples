using System;
using System.IO;
using Aspose.Pdf;

public class ApplyCustomColorScheme
{
    public static void Main()
    {
        // Resolve absolute paths relative to the executable directory.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string xmlFilePath = Path.Combine(baseDir, "sample.xml");
        string xslFilePath = Path.Combine(baseDir, "style.xsl");

        // ---------------------------------------------------------------------
        // Ensure that the XML source file exists. If it does not, create a minimal
        // example on‑the‑fly. This prevents the FileNotFoundException that caused
        // the original build failure.
        // ---------------------------------------------------------------------
        if (!File.Exists(xmlFilePath))
        {
            string xmlContent = @"<?xml version='1.0' encoding='UTF-8'?>
<Report>
    <Title>Sales Report</Title>
    <Section>
        <Header>Q1</Header>
        <Value>15000</Value>
    </Section>
    <Section>
        <Header>Q2</Header>
        <Value>20000</Value>
    </Section>
</Report>";
            File.WriteAllText(xmlFilePath, xmlContent);
        }

        // ---------------------------------------------------------------------
        // Ensure that the XSL stylesheet exists. The stylesheet defines a custom
        // colour scheme (e.g., blue title, dark‑gray body) using Aspose.Pdf XML
        // tags. If the file is missing we create a simple stylesheet that works
        // with the sample XML above.
        // ---------------------------------------------------------------------
        if (!File.Exists(xslFilePath))
        {
            string xslContent = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
                xmlns:pdf='http://schemas.aspose.com/pdf'>
  <xsl:output method='xml'/>
  <xsl:template match='/'>
    <pdf:Document>
      <pdf:Page>
        <pdf:Paragraph>
          <pdf:TextFragment>
            <xsl:apply-templates select='Report/Title'/>
          </pdf:TextFragment>
        </pdf:Paragraph>
        <xsl:apply-templates select='Report/Section'/>
      </pdf:Page>
    </pdf:Document>
  </xsl:template>

  <xsl:template match='Title'>
    <pdf:TextFragment Font='Helvetica' FontSize='20' Color='Blue'>
      <xsl:value-of select='.'/>
    </pdf:TextFragment>
  </xsl:template>

  <xsl:template match='Section'>
    <pdf:Paragraph>
      <pdf:TextFragment Font='Helvetica' FontSize='12' Color='DarkGray'>
        <xsl:value-of select='Header'/>: <xsl:value-of select='Value'/>
      </pdf:TextFragment>
    </pdf:Paragraph>
  </xsl:template>
</xsl:stylesheet>";
            File.WriteAllText(xslFilePath, xslContent);
        }

        // Create an empty PDF document and bind the XML using the XSL stylesheet.
        using (Document pdfDocument = new Document())
        {
            pdfDocument.BindXml(xmlFilePath, xslFilePath);
            // Save the resulting PDF next to the source files.
            string outputPath = Path.Combine(baseDir, "output.pdf");
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF generated successfully: {outputPath}");
        }
    }
}
