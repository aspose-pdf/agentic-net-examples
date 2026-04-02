using System;
using System.IO;
using Aspose.Pdf;

public class LoadXmlFromMemoryStream
{
    public static void Main()
    {
        // Sample XML content
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><document><section><paragraph>Hello, World!</paragraph></section></document>";
        byte[] xmlBytes = System.Text.Encoding.UTF8.GetBytes(xml);
        using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
        {
            DocumentFactory documentFactory = new DocumentFactory();
            using (Document pdfDocument = documentFactory.CreateDocument())
            {
                pdfDocument.BindXml(xmlStream);
                pdfDocument.Save("output.pdf");
            }
        }
    }
}