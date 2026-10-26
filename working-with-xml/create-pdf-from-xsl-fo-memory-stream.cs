using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Example XML that will be converted to PDF.
        string xmlContent = @"<?xml version='1.0' encoding='UTF-8'?>
<document>
    <page>
        <paragraph>Hello, world!</paragraph>
    </page>
</document>";

        // Write the XML string into a memory stream.
        using (MemoryStream xmlStream = new MemoryStream())
        {
            using (StreamWriter writer = new StreamWriter(xmlStream))
            {
                writer.Write(xmlContent);
                writer.Flush();
                xmlStream.Position = 0; // Reset stream position for reading.

                // Create an empty PDF document and bind the XML content.
                using (Document pdfDoc = new Document())
                {
                    pdfDoc.BindXml(xmlStream);
                    // Save the document as PDF using default settings.
                    pdfDoc.Save("output.pdf");
                }
            }
        }

        Console.WriteLine("PDF created successfully.");
    }
}
