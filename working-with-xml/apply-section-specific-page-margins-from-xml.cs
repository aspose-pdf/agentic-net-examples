using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string marginXmlPath = "margins.xml"; // XML with margin definitions
        const string outputPdfPath = "output.pdf";  // result PDF

        // Verify that required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(marginXmlPath))
        {
            Console.Error.WriteLine("Input PDF or margin XML file not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Load margin definitions from the XML file.
        // Expected XML format:
        // <Margins>
        //   <Section name="Header" top="50" bottom="10" left="20" right="20"/>
        //   <Section name="Body"   top="30" bottom="30" left="40" right="40"/>
        // </Margins>
        // -----------------------------------------------------------------
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(marginXmlPath);

        // Map section name -> MarginInfo instance
        Dictionary<string, MarginInfo> marginMap = new Dictionary<string, MarginInfo>(StringComparer.OrdinalIgnoreCase);
        foreach (XmlNode node in xmlDoc.SelectNodes("//Section"))
        {
            string name   = node.Attributes["name"]?.Value ?? "Default";
            double top    = double.Parse(node.Attributes["top"]?.Value    ?? "0");
            double bottom = double.Parse(node.Attributes["bottom"]?.Value ?? "0");
            double left   = double.Parse(node.Attributes["left"]?.Value   ?? "0");
            double right  = double.Parse(node.Attributes["right"]?.Value  ?? "0");

            // Build a MarginInfo object with individual side values
            MarginInfo mi = new MarginInfo();
            mi.Top    = top;
            mi.Bottom = bottom;
            mi.Left   = left;
            mi.Right  = right;

            marginMap[name] = mi;
        }

        // -----------------------------------------------------------------
        // Open the PDF, apply margins conditionally, and save.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Example conditional logic:
            // - Page 1 uses the "Header" margin definition.
            // - All other pages use the "Body" margin definition.
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                string key = pageIndex == 1 ? "Header" : "Body";

                if (marginMap.TryGetValue(key, out MarginInfo marginInfo))
                {
                    // Apply the margin information to the page.
                    page.PageInfo.Margin = marginInfo;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with applied margins to '{outputPdfPath}'.");
    }
}
