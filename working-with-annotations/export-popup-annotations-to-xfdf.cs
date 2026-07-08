using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXfdfPath = "popups.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export all annotations to a memory stream in XFDF format
            using (MemoryStream allXfdfStream = new MemoryStream())
            {
                pdfDoc.ExportAnnotationsToXfdf(allXfdfStream);
                allXfdfStream.Position = 0; // reset for reading

                // Load the XFDF XML
                XDocument xfdfDoc = XDocument.Load(allXfdfStream);

                // XFDF stores annotations as <annotation> elements.
                // Keep only those where the @subtype attribute equals "Popup".
                var popupAnnotations = xfdfDoc
                    .Descendants("annotation")
                    .Where(a => (string)a.Attribute("subtype") == "Popup")
                    .ToList();

                // Create a new XFDF document containing only the popup annotations
                XDocument popupXfdf = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement("xfdf",
                        new XAttribute("xml:space", "preserve"),
                        // Preserve the <pdf> element if present (metadata about the PDF)
                        xfdfDoc.Root.Element("pdf") ?? null,
                        // Add the filtered popup annotations
                        popupAnnotations
                    )
                );

                // Save the filtered XFDF to the desired file
                popupXfdf.Save(outputXfdfPath);
                Console.WriteLine($"Popup annotations exported to '{outputXfdfPath}'.");
            }
        }
    }
}