using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class ExportPopupAnnotations
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "popup_annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Export all annotations to an in‑memory XFDF stream
            using (MemoryStream allXfdfStream = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(allXfdfStream);
                allXfdfStream.Position = 0; // rewind for reading

                // Load the XFDF XML
                XDocument xfdfDoc = XDocument.Load(allXfdfStream);

                // XFDF stores annotations under the <annots> element.
                // Keep only those with Subtype="Popup".
                var popupAnnots = xfdfDoc
                    .Descendants("annot")
                    .Where(a => (string)a.Attribute("subtype") == "Popup")
                    .ToList();

                // Remove all other annotation elements
                xfdfDoc
                    .Descendants("annot")
                    .Where(a => (string)a.Attribute("subtype") != "Popup")
                    .Remove();

                // If there were no popup annotations, create an empty <annots> element
                if (!popupAnnots.Any())
                {
                    // Ensure the <annots> element exists (some viewers expect it)
                    var annotsElem = xfdfDoc.Root.Element("annots");
                    if (annotsElem == null)
                    {
                        xfdfDoc.Root.Add(new XElement("annots"));
                    }
                }

                // Save the filtered XFDF to the target file
                xfdfDoc.Save(outputXfdf);
                Console.WriteLine($"Popup annotations exported to '{outputXfdf}'.");
            }
        }
    }
}