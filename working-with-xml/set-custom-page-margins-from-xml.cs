using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string marginsXmlPath = "margins.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(marginsXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {marginsXmlPath}");
            return;
        }

        // Load margin definitions from XML.
        XDocument xmlDoc = XDocument.Load(marginsXmlPath);
        var sections = xmlDoc.Root?.Elements("Section");

        if (sections == null)
        {
            Console.Error.WriteLine("No sections defined in XML.");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each section and apply margins to the specified page range.
            foreach (var sec in sections)
            {
                // Parse page range.
                int startPage = sec.Attribute("StartPage") != null ? int.Parse(sec.Attribute("StartPage").Value) : 1;
                int endPage   = sec.Attribute("EndPage")   != null ? int.Parse(sec.Attribute("EndPage").Value)   : pdfDoc.Pages.Count;

                // Clamp range to actual page count.
                if (startPage < 1) startPage = 1;
                if (endPage > pdfDoc.Pages.Count) endPage = pdfDoc.Pages.Count;
                if (startPage > endPage) continue;

                // Parse margin values.
                var marginsElem = sec.Element("Margins");
                if (marginsElem == null) continue;

                double left   = marginsElem.Attribute("Left")   != null ? double.Parse(marginsElem.Attribute("Left").Value)   : 0;
                double right  = marginsElem.Attribute("Right")  != null ? double.Parse(marginsElem.Attribute("Right").Value)  : 0;
                double top    = marginsElem.Attribute("Top")    != null ? double.Parse(marginsElem.Attribute("Top").Value)    : 0;
                double bottom = marginsElem.Attribute("Bottom") != null ? double.Parse(marginsElem.Attribute("Bottom").Value) : 0;

                // Apply margins to each page in the range.
                for (int i = startPage; i <= endPage; i++) // 1‑based indexing
                {
                    Page page = pdfDoc.Pages[i];
                    // Use MarginInfo (not the non‑existent Margin class) to set page margins.
                    page.PageInfo.Margin = new MarginInfo(left, right, top, bottom);
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with custom margins to '{outputPdfPath}'.");
    }
}
