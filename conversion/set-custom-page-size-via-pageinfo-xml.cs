using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_custom_pageinfo.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Export the PDF to its XML representation (in memory)
            using (MemoryStream xmlStream = new MemoryStream())
            {
                doc.Save(xmlStream, new XmlSaveOptions());
                xmlStream.Position = 0;

                // Load the XML for manipulation
                XDocument xDoc = XDocument.Load(xmlStream);
                XNamespace ns = xDoc.Root.GetDefaultNamespace();

                // Set custom Width and Height on each PageInfo element
                foreach (XElement pageInfo in xDoc.Descendants(ns + "PageInfo"))
                {
                    // Example dimensions (A4 size in points)
                    pageInfo.SetAttributeValue("Width",  "595"); // 8.27 in * 72
                    pageInfo.SetAttributeValue("Height", "842"); // 11.69 in * 72
                }

                // Convert the modified XML back to a string
                string modifiedXml;
                using (StringWriter writer = new StringWriter())
                {
                    xDoc.Save(writer);
                    modifiedXml = writer.ToString();
                }

                // Bind the modified XML back to the PDF document
                doc.BindXml(modifiedXml);
            }

            // Save the updated PDF with the new page dimensions
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom PageInfo to '{outputPdf}'.");
    }
}