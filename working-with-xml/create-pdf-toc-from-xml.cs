using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";   // existing PDF to augment
        const string xmlPath = "headings.xml"; // XML with heading hierarchy
        const string outputPdfPath = "output_with_toc.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Parse the XML file. Expected format:
            // <Headings>
            //   <Heading Level="1" Title="Chapter 1" Page="2" />
            //   <Heading Level="2" Title="Section 1.1" Page="3" />
            //   ...
            // </Headings>
            XDocument xmlDoc = XDocument.Load(xmlPath);
            var headingElements = xmlDoc.Root?.Elements("Heading") ?? Array.Empty<XElement>();

            // Insert a new page at the beginning for the Table of Contents
            Page tocPage = doc.Pages.Add();
            doc.Pages.Insert(1, tocPage);

            // Add a visible title for the TOC
            TextFragment titleFragment = new TextFragment("Table of Contents");
            titleFragment.TextState.FontSize = 18;
            titleFragment.Position = new Position(50, tocPage.PageInfo.Height - 50);
            tocPage.Paragraphs.Add(titleFragment);

            // Starting vertical position for the first entry (some space after the title)
            double currentY = tocPage.PageInfo.Height - 80;
            const double lineHeight = 20; // space between entries

            foreach (var h in headingElements)
            {
                // Safely read attributes; if missing, skip the entry
                if (h.Attribute("Level") == null || h.Attribute("Title") == null || h.Attribute("Page") == null)
                    continue;

                int level = (int)h.Attribute("Level");
                string title = (string)h.Attribute("Title");
                int pageNum = (int)h.Attribute("Page");

                // Adjust page number because we inserted the TOC page at position 1
                int targetPageNumber = pageNum + 1;

                // Create the visible TOC entry
                TextFragment entryFragment = new TextFragment(title);
                entryFragment.TextState.FontSize = 12;
                double indent = 50 + (level - 1) * 20; // simple left indent per level
                entryFragment.Position = new Position(indent, currentY);
                tocPage.Paragraphs.Add(entryFragment);

                // Create a clickable link annotation that jumps to the target page
                double fontSize = entryFragment.TextState.FontSize;
                Rectangle linkRect = new Rectangle(
                    indent,
                    currentY - fontSize,
                    tocPage.PageInfo.Width - 50,
                    currentY);

                // Use the correct GoToAction constructor (expects a Page instance)
                Page targetPage = doc.Pages[targetPageNumber];
                GoToAction goTo = new GoToAction(targetPage);
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect)
                {
                    Action = goTo
                };
                tocPage.Annotations.Add(link);

                // Move to the next line
                currentY -= lineHeight;
            }

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPdfPath}'.");
    }
}
