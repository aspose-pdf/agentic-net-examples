using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create the XFDF root element
            XNamespace xfdfNs = "http://ns.adobe.com/xfdf/";
            XElement xfdfRoot = new XElement(xfdfNs + "xfdf",
                new XElement(xfdfNs + "annots")
            );

            XElement annotsElement = xfdfRoot.Element(xfdfNs + "annots");

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through each annotation on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Basic annotation data
                    Aspose.Pdf.Rectangle rect = annot.Rect;
                    string typeName = annot.GetType().Name; // e.g., TextAnnotation, LinkAnnotation

                    XElement annotElem = new XElement(xfdfNs + "annotation",
                        new XAttribute("type", typeName),
                        new XAttribute("page", i),
                        new XElement(xfdfNs + "rect",
                            new XAttribute("llx", rect.LLX),
                            new XAttribute("lly", rect.LLY),
                            new XAttribute("urx", rect.URX),
                            new XAttribute("ury", rect.URY)
                        )
                    );

                    // Add type‑specific fields when available
                    if (annot is TextAnnotation textAnnot)
                    {
                        annotElem.Add(new XElement(xfdfNs + "contents", textAnnot.Contents));
                        annotElem.Add(new XElement(xfdfNs + "title", textAnnot.Title));
                    }
                    else if (annot is LinkAnnotation linkAnnot)
                    {
                        annotElem.Add(new XElement(xfdfNs + "action", linkAnnot.Action));
                    }

                    annotsElement.Add(annotElem);
                }
            }

            // Write XFDF to a memory stream (no file is created)
            using (MemoryStream ms = new MemoryStream())
            {
                XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), xfdfRoot);
                xdoc.Save(ms);
                ms.Position = 0;

                // Example: output XFDF content to console
                using (StreamReader reader = new StreamReader(ms))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine(xfdfContent);
                }
            }
        }
    }
}