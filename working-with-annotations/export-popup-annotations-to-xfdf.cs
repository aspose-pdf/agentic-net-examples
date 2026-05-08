using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXfdf = "popups.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdf))
        {
            // Export all annotations to an in‑memory XFDF stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                srcDoc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // rewind for reading

                // Load the XFDF XML
                XDocument xfdfXml = XDocument.Load(xfdfStream);

                // The XFDF structure: <xfdf><annots>...</annots></xfdf>
                // Keep only <popup> elements (and their children)
                XElement root = xfdfXml.Root;
                if (root != null)
                {
                    XElement annots = root.Element("annots");
                    if (annots != null)
                    {
                        // Remove any annotation element that is not a popup
                        foreach (XElement elem in annots.Elements())
                        {
                            // Popup annotations are represented by <popup> elements
                            if (elem.Name != "popup")
                            {
                                elem.Remove();
                            }
                        }
                    }
                }

                // Save the filtered XFDF to the output file
                xfdfXml.Save(outputXfdf);
                Console.WriteLine($"Popup annotations exported to '{outputXfdf}'.");
            }
        }
    }
}