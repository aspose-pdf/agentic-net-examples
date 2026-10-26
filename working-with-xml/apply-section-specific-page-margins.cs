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
        public MarginInfo Margins { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string marginXmlPath  = "margins.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(marginXmlPath))
        {
            Console.Error.WriteLine($"Margin definition XML not found: {marginXmlPath}");
            return;
        }

        // Load margin definitions from XML
        List<SectionMargin> sections = LoadMarginDefinitions(marginXmlPath);

        // Open the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Find the first section that covers the current page
                SectionMargin matchingSection = sections.Find(s => pageNum >= s.StartPage && pageNum <= s.EndPage);
                if (matchingSection != null)
                {
                    // Apply the margin to the page
                    doc.Pages[pageNum].PageInfo.Margin = new MarginInfo
                    {
                        Left   = matchingSection.Margins.Left,
                        Right  = matchingSection.Margins.Right,
                        Top    = matchingSection.Margins.Top,
                        Bottom = matchingSection.Margins.Bottom
                    };
                }
            }

            // Save the modified PDF (save-to-non-pdf-always-use-save-options not needed here)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with section‑specific margins to '{outputPdfPath}'.");
    }

    // Parses the XML file and returns a list of SectionMargin objects
    static List<SectionMargin> LoadMarginDefinitions(string xmlPath)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        var sections = new List<SectionMargin>();

        foreach (XElement sec in xdoc.Root.Elements("Section"))
        {
            int start = (int)sec.Attribute("start");
            int end   = (int)sec.Attribute("end");

            XElement m = sec.Element("Margins");
            MarginInfo marginInfo = new MarginInfo {
                Left   = (double)m.Attribute("left"),
                Right  = (double)m.Attribute("right"),
                Top    = (double)m.Attribute("top"),
                Bottom = (double)m.Attribute("bottom")
            };

            sections.Add(new SectionMargin
            {
                StartPage = start,
                EndPage   = end,
                Margins   = marginInfo
            });
        }

        return sections;
    }
}