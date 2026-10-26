using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "backgrounds.xml"; // XML defining background images
        const string outputPdf = "output.pdf";     // result PDF

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

        // Load XML: expected format <Backgrounds><Page number="1" image="brand1.png"/></Backgrounds>
        XDocument xDoc = XDocument.Load(xmlPath);
        // Build a simple lookup of page number -> image path
        var bgMap = new System.Collections.Generic.Dictionary<int, string>();
        foreach (var elem in xDoc.Root.Elements("Page"))
        {
            int pageNum;
            if (int.TryParse(elem.Attribute("number")?.Value, out pageNum) &&
                !string.IsNullOrWhiteSpace(elem.Attribute("image")?.Value))
            {
                string imgPath = elem.Attribute("image").Value;
                if (File.Exists(imgPath))
                    bgMap[pageNum] = imgPath;
                else
                    Console.Error.WriteLine($"Image not found for page {pageNum}: {imgPath}");
            }
        }

        // Process PDF
        using (Document doc = new Document(pdfPath))
        {
            // Iterate using 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (!bgMap.ContainsKey(i))
                    continue; // no background defined for this page

                string imgPath = bgMap[i];
                // Create an ImageStamp that will be placed behind page content
                ImageStamp stamp = new ImageStamp(imgPath)
                {
                    Background = true, // render behind existing content
                    // Stretch to fill the whole page
                    Width = doc.Pages[i].PageInfo.Width,
                    Height = doc.Pages[i].PageInfo.Height,
                    LeftMargin = 0,
                    TopMargin = 0
                };

                // Add the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background images applied. Output saved to '{outputPdf}'.");
    }
}