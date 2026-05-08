using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Printing; // for PageSettings if needed

class Program
{
    static void Main()
    {
        const string xmlPath   = "layout.xml";   // XML describing page layout
        const string pdfInput  = "input.pdf";    // source PDF
        const string pdfOutput = "output.pdf";   // result PDF

        if (!File.Exists(xmlPath) || !File.Exists(pdfInput))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Parse the XML to determine desired orientation.
        // Expected format: <Document><Page orientation="landscape"/></Document>
        bool makeLandscape = false;
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlNode pageNode = doc.SelectSingleNode("//Page[@orientation]");
            if (pageNode != null)
            {
                string orientation = pageNode.Attributes["orientation"]?.Value?.Trim().ToLowerInvariant();
                makeLandscape = orientation == "landscape";
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            return;
        }

        // Load the PDF, apply orientation, and save.
        using (Document pdfDoc = new Document(pdfInput))
        {
            // Apply orientation to each page (or you could target specific pages).
            foreach (Page page in pdfDoc.Pages)
            {
                // PageInfo.IsLandscape controls the page orientation.
                page.PageInfo.IsLandscape = makeLandscape;

                // Optionally, adjust the page size to reflect landscape dimensions.
                // Swapping width/height ensures content fits correctly.
                if (makeLandscape && !page.PageInfo.IsLandscape)
                {
                    // No action needed; setting IsLandscape already swaps dimensions internally.
                }
                else if (!makeLandscape && page.PageInfo.IsLandscape)
                {
                    // Reset to portrait if needed.
                    page.PageInfo.IsLandscape = false;
                }

                // If you need to control printing orientation, you can also set PageSettings.
                // This is optional and only affects printing, not the PDF itself.
                // page.PageSettings.Landscape = makeLandscape; // requires Aspose.Pdf.Printing
            }

            pdfDoc.Save(pdfOutput);
        }

        Console.WriteLine($"PDF saved to '{pdfOutput}' with {(makeLandscape ? "landscape" : "portrait")} orientation.");
    }
}