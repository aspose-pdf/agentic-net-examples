using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;               // For Color parsing if needed (Color.Parse is in Aspose.Pdf)

class ApplyPageBackground
{
    static void Main()
    {
        // Input PDF and XML files
        const string pdfPath = "input.pdf";
        const string xmlPath = "colors.xml";
        const string outputPath = "output.pdf";

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

        // Load XML containing color definitions.
        // Expected format:
        // <Colors>
        //   <Page index="1" value="#FFCCCC"/>
        //   <Page index="2" value="#CCFFCC"/>
        //   ...
        // </Colors>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Build a lookup of page index -> color string
        var pageColors = new System.Collections.Generic.Dictionary<int, string>();
        foreach (var pageElem in xmlDoc.Root.Elements("Page"))
        {
            int index;
            if (int.TryParse(pageElem.Attribute("index")?.Value, out index) &&
                !string.IsNullOrWhiteSpace(pageElem.Attribute("value")?.Value))
            {
                pageColors[index] = pageElem.Attribute("value").Value.Trim();
            }
        }

        // Open the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // If a custom color is defined for this page, apply it
                if (pageColors.TryGetValue(i, out string colorStr))
                {
                    // Parse the XML color string (e.g., "#RRGGBB" or "rgb(255,0,0)")
                    // Aspose.Pdf.Color.Parse handles hex and named colors.
                    Aspose.Pdf.Color bgColor = Aspose.Pdf.Color.Parse(colorStr);
                    page.Background = bgColor;
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Background colors applied. Saved to '{outputPath}'.");
    }
}