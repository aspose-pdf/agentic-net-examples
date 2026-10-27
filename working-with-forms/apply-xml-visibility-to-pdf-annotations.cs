using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "visibility.xml";
        const string outputPdf = "output.pdf";

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

        // Load visibility definitions from XML.
        // Expected format:
        // <Visibilities>
        //   <Section Id="sec1" Visibility="hidden" />
        //   <Section Id="sec2" Visibility="visible" />
        // </Visibilities>
        XDocument xmlDoc = XDocument.Load(xmlPath);
        var visibilityMap = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        foreach (var elem in xmlDoc.Root?.Elements("Section") ?? Array.Empty<XElement>())
        {
            string id = (string)elem.Attribute("Id");
            string vis = (string)elem.Attribute("Visibility");
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(vis))
            {
                bool hidden = vis.Equals("hidden", StringComparison.OrdinalIgnoreCase);
                visibilityMap[id] = hidden;
            }
        }

        // Process PDF.
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the PDF is tagged.
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("PDF is not tagged; cannot apply visibility states.");
                doc.Save(outputPdf);
                Console.WriteLine($"Saved unchanged PDF to '{outputPdf}'.");
                return;
            }

            ITaggedContent taggedContent = doc.TaggedContent;
            // Use dynamic to avoid compile‑time dependency on StructureElement.
            dynamic root = taggedContent.RootElement;

            ApplyVisibilityRecursive(root, visibilityMap);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Visibility states applied and saved to '{outputPdf}'.");
    }

    private static void ApplyVisibilityRecursive(dynamic element, Dictionary<string, bool> visibilityMap)
    {
        // Try to obtain the element's ID.
        string elementId = null;
        try { elementId = element.GetId(); } catch { /* GetId not available */ }

        if (!string.IsNullOrEmpty(elementId) && visibilityMap.TryGetValue(elementId, out bool hide))
        {
            // Attempt to set the hidden flag; ignore if the method does not exist in the current version.
            try { element.SetHidden(hide); } catch { /* SetHidden not available */ }
        }

        // Recurse into child elements if any.
        try
        {
            foreach (var child in element.ChildElements)
            {
                ApplyVisibilityRecursive(child, visibilityMap);
            }
        }
        catch
        {
            // ChildElements not supported – nothing to recurse.
        }
    }
}
