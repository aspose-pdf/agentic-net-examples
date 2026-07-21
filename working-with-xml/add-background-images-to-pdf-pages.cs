using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string xmlPath       = "backgrounds.xml";   // XML defining backgrounds
        const string outputPdfPath = "output.pdf";        // result PDF

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Background XML not found: {xmlPath}");
            return;
        }

        // Load XML. Expected format:
        // <Backgrounds>
        //   <Page number="1" image="bg1.png" />
        //   <Page number="2" image="bg2.png" />
        // </Backgrounds>
        XDocument xDoc = XDocument.Load(xmlPath);
        var backgroundMap = xDoc.Root?
            .Elements("Page")
            .Select(e => new
            {
                Number = (int?)e.Attribute("number") ?? 0,
                ImagePath = (string)e.Attribute("image")
            })
            .Where(p => p.Number > 0 && !string.IsNullOrEmpty(p.ImagePath))
            .ToDictionary(p => p.Number, p => p.ImagePath) ?? new System.Collections.Generic.Dictionary<int, string>();

        // Open PDF inside a using block (deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                int pageNumber = page.Number;
                if (backgroundMap.TryGetValue(pageNumber, out string imgPath) && File.Exists(imgPath))
                {
                    // Create an Image object (parameterless ctor, then set File)
                    Aspose.Pdf.Image bgImage = new Aspose.Pdf.Image();
                    bgImage.File = imgPath;

                    // Assign as page background (generator‑only property)
                    page.BackgroundImage = bgImage;
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with background images saved to '{outputPdfPath}'.");
    }
}