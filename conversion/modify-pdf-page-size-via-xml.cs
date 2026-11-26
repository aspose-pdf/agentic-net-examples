using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath   = "input.xml";          // source XML describing the PDF
        const string xmlTempPath    = "temp_modified.xml"; // intermediate XML after changes
        const string pdfOutputPath  = "output.pdf";         // final PDF file

        // Verify the XML source exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Load the XML, locate the first <PageInfo> element and set custom dimensions
        XDocument xDoc = XDocument.Load(xmlInputPath);
        XElement pageInfo = xDoc.Descendants("PageInfo").FirstOrDefault();

        if (pageInfo != null)
        {
            // Example custom size: A4 portrait (Width=595, Height=842 points)
            pageInfo.SetAttributeValue("Width",  "595");
            pageInfo.SetAttributeValue("Height", "842");
        }
        else
        {
            Console.Error.WriteLine("No <PageInfo> element found in the XML.");
            return;
        }

        // Save the modified XML to a temporary file
        xDoc.Save(xmlTempPath);

        // Load the modified XML into an Aspose.Pdf Document, then save as PDF
        using (Document pdfDoc = new Document())
        {
            // BindXml is the correct method to import a PDF structure from an XML file
            pdfDoc.BindXml(xmlTempPath);
            pdfDoc.Save(pdfOutputPath); // Document.Save writes PDF regardless of extension
        }

        // Clean up the temporary XML file
        try { File.Delete(xmlTempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF generated successfully: {pdfOutputPath}");
    }
}
