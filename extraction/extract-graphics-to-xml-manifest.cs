using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string manifestPath = "manifest.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that page 25 exists (pages are 1‑based)
            if (doc.Pages.Count < 25)
            {
                Console.Error.WriteLine("Document contains fewer than 25 pages.");
                return;
            }

            // Get page 25
            Page page = doc.Pages[25];

            // Absorb vector graphics objects from the page
            using (GraphicsAbsorber absorber = new GraphicsAbsorber())
            {
                // GraphicsAbsorber lives in Aspose.Pdf.Vector and works via Visit(page)
                absorber.Visit(page);

                // Build XML manifest
                XElement manifestRoot = new XElement("Manifest");
                int elementCounter = 1; // fallback ID when GraphicElement has no Id property

                foreach (GraphicElement gElem in absorber.Elements)
                {
                    // Rectangle of the element
                    Rectangle rect = gElem.Rectangle;

                    // Operators list – represent as a comma‑separated string of operator type names
                    string ops = string.Join(",", gElem.Operators.Select(op => op.GetType().Name));

                    XElement elementNode = new XElement("Element",
                        new XElement("Id", elementCounter++),
                        new XElement("Rectangle",
                            new XAttribute("llx", rect.LLX),
                            new XAttribute("lly", rect.LLY),
                            new XAttribute("urx", rect.URX),
                            new XAttribute("ury", rect.URY)),
                        new XElement("Operators", ops));

                    manifestRoot.Add(elementNode);
                }

                XDocument manifestDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), manifestRoot);
                manifestDoc.Save(manifestPath);
                Console.WriteLine($"XML manifest saved to '{manifestPath}'.");
            }
        }
    }
}
