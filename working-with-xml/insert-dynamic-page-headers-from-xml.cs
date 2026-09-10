using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfInputPath = "template.pdf";   // existing PDF
        const string xmlDataPath = "headers.xml";    // XML with per‑page header text
        const string pdfOutputPath = "output_with_headers.pdf";

        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlDataPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(pdfInputPath))
        {
            // Parse XML – expected format:
            // <Headers>
            //   <Header page="1">First page header</Header>
            //   <Header page="2">Second page header</Header>
            //   ...
            // </Headers>
            XDocument xDoc = XDocument.Load(xmlDataPath);
            var headerMap = new Dictionary<int, string>();
            foreach (var elem in xDoc.Root?.Elements("Header") ?? new List<XElement>())
            {
                if (int.TryParse(elem.Attribute("page")?.Value, out int pageNum))
                {
                    headerMap[pageNum] = elem.Value;
                }
            }

            // Iterate over each page and add a TextStamp as the header
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Retrieve the header for the current page, fallback to empty string
                headerMap.TryGetValue(i, out string headerText);
                headerText ??= string.Empty;

                // Create a TextStamp – this works without any Facades types
                TextStamp stamp = new TextStamp(headerText)
                {
                    // Position the stamp at the top centre of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Use TopMargin to push the stamp down from the top edge
                    TopMargin = 20 // 20 points from the top edge
                };

                // Configure appearance (font, size, color, etc.)
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 12;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                pdfDoc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF with dynamic headers saved to '{pdfOutputPath}'.");
    }
}
