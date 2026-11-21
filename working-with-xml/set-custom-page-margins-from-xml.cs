using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "margins.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load margin definitions from XML.
        // Expected format:
        // <Sections>
        //   <Section StartPage="1" EndPage="3" Top="20" Bottom="20" Left="30" Right="30" />
        //   <Section StartPage="4" EndPage="5" Top="10" Bottom="10" Left="15" Right="15" />
        // </Sections>
        XDocument xmlDoc = XDocument.Load(xmlPath);
        var sections = xmlDoc.Root.Elements("Section");

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each section definition and apply margins to the specified page range.
            foreach (var sec in sections)
            {
                // Parse attributes; default to 0 if missing.
                int startPage = (int?)sec.Attribute("StartPage") ?? 1;
                int endPage   = (int?)sec.Attribute("EndPage")   ?? pdfDoc.Pages.Count;
                double top    = (double?)sec.Attribute("Top")    ?? 0;
                double bottom = (double?)sec.Attribute("Bottom") ?? 0;
                double left   = (double?)sec.Attribute("Left")   ?? 0;
                double right  = (double?)sec.Attribute("Right")  ?? 0;

                // Ensure page indices are within the document bounds.
                if (startPage < 1) startPage = 1;
                if (endPage > pdfDoc.Pages.Count) endPage = pdfDoc.Pages.Count;

                // Apply margins to each page in the range (1‑based indexing).
                for (int i = startPage; i <= endPage; i++)
                {
                    Page page = pdfDoc.Pages[i];
                    page.PageInfo.Margin = new MarginInfo
                    {
                        Top    = top,
                        Bottom = bottom,
                        Left   = left,
                        Right  = right
                    };
                }
            }

            // Save the modified PDF. No SaveOptions needed because the output format is PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Margins applied and saved to '{outputPdf}'.");
    }
}
