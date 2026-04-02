using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages
        string pdfPath = "sample.pdf";
        using (Document pdfDoc = new Document())
        {
            // Add three blank pages
            pdfDoc.Pages.Add();
            pdfDoc.Pages.Add();
            pdfDoc.Pages.Add();
            pdfDoc.Save(pdfPath);
        }

        // Create a sample XML file with section titles
        string xmlPath = "sections.xml";
        string xmlContent = "<?xml version='1.0' encoding='utf-8'?><document><section title='Introduction' /><section title='Chapter 1' /><section title='Conclusion' /></document>";
        File.WriteAllText(xmlPath, xmlContent);

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load XML and extract titles
            XDocument xDoc = XDocument.Load(xmlPath);
            foreach (XElement sectionElement in xDoc.Root.Elements("section"))
            {
                XAttribute titleAttr = sectionElement.Attribute("title");
                if (titleAttr != null)
                {
                    // Create a new outline item
                    OutlineItemCollection outlineItem = new OutlineItemCollection(pdfDoc.Outlines);
                    outlineItem.Title = titleAttr.Value;
                    // Link the outline item to the first page (using GoToAction instead of Destination)
                    outlineItem.Action = new GoToAction(pdfDoc.Pages[1]);
                    // Add the outline item to the document outline hierarchy
                    pdfDoc.Outlines.Add(outlineItem);
                }
            }

            // Save the updated PDF with outlines
            pdfDoc.Save("output.pdf");
        }
    }
}
