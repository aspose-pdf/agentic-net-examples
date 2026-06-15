using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    // Simple DTO to hold margin settings for a page range
    class SectionMargin
    {
        public int StartPage { get; set; }
        public int EndPage   { get; set; }
        public int Top       { get; set; }   // points
        public int Left      { get; set; }
        public int Right     { get; set; }
        public int Bottom    { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string marginsXmlPath = "margins.xml";

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

        // Parse XML – expected format:
        // <Margins>
        //   <Section startPage="1" endPage="3">
        //     <Top>20</Top><Left>15</Left><Right>15</Right><Bottom>20</Bottom>
        //   </Section>
        //   ...
        // </Margins>
        var sections = new List<SectionMargin>();
        XDocument xmlDoc = XDocument.Load(marginsXmlPath);
        foreach (var secElem in xmlDoc.Root.Elements("Section"))
        {
            var sec = new SectionMargin
            {
                StartPage = (int)secElem.Attribute("startPage"),
                EndPage   = (int)secElem.Attribute("endPage"),
                Top       = (int?)secElem.Element("Top")   ?? 0,
                Left      = (int?)secElem.Element("Left")  ?? 0,
                Right     = (int?)secElem.Element("Right") ?? 0,
                Bottom    = (int?)secElem.Element("Bottom")?? 0
            };
            sections.Add(sec);
        }

        try
        {
            // Open PDF – use the lifecycle rule (using block)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    // Find the first section that covers this page
                    SectionMargin match = null;
                    foreach (var sec in sections)
                    {
                        if (i >= sec.StartPage && i <= sec.EndPage)
                        {
                            match = sec;
                            break;
                        }
                    }

                    if (match != null)
                    {
                        // Create and assign a new MarginInfo instance using the correct property names
                        var marginInfo = new MarginInfo
                        {
                            Top    = match.Top,
                            Bottom = match.Bottom,
                            Left   = match.Left,
                            Right  = match.Right
                        };
                        pdfDoc.Pages[i].PageInfo.Margin = marginInfo;
                    }
                }

                // Save the modified PDF – simple Save (no extra SaveOptions needed for PDF output)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved with custom margins to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
