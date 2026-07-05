using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "backgrounds.xml";
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

        // Load XML that defines background images per page.
        // Expected format:
        // <Backgrounds>
        //   <Background page="1" image="bg1.png" />
        //   <Background page="2" image="bg2.png" />
        // </Backgrounds>
        XDocument xdoc = XDocument.Load(xmlPath);
        var backgroundMap = new Dictionary<int, string>();

        foreach (var elem in xdoc.Root.Elements("Background"))
        {
            if (int.TryParse(elem.Attribute("page")?.Value, out int pageNumber))
            {
                string imgPath = elem.Attribute("image")?.Value;
                if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                {
                    backgroundMap[pageNumber] = imgPath;
                }
            }
        }

        // Open the PDF, apply background images, and save.
        using (Document doc = new Document(pdfPath))
        {
            foreach (var kvp in backgroundMap)
            {
                int pageNum = kvp.Key; // 1‑based indexing
                if (pageNum >= 1 && pageNum <= doc.Pages.Count)
                {
                    Page page = doc.Pages[pageNum];

                    // Create an Image object and assign it as the page background.
                    Aspose.Pdf.Image bgImage = new Aspose.Pdf.Image();
                    bgImage.File = kvp.Value;
                    page.BackgroundImage = bgImage;
                }
            }

            doc.Save(outputPath); // Save the modified PDF.
        }

        Console.WriteLine($"PDF with background images saved to '{outputPath}'.");
    }
}