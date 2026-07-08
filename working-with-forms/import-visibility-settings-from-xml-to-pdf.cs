using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, XML with visibility settings and output PDF paths
        const string pdfPath = "input.pdf";
        const string xmlPath = "visibility.xml";
        const string outputPath = "output.pdf";

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the PDF document (core API)
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
        {
            // Load and parse the XML visibility settings
            XDocument visibilityDoc = XDocument.Load(xmlPath);

            // Guard against malformed XML
            if (visibilityDoc.Root == null)
            {
                Console.Error.WriteLine("Invalid XML: missing root element.");
                return;
            }

            // Expected XML format:
            // <VisibilitySettings>
            //   <Section id="SectionNameOrId" visible="true|false" />
            //   ...
            // </VisibilitySettings>

            foreach (XElement sectionElement in visibilityDoc.Root.Elements("Section"))
            {
                string sectionId = (string)sectionElement.Attribute("id");
                if (string.IsNullOrEmpty(sectionId))
                    continue; // skip entries without an id

                bool isVisible = true;
                XAttribute visibleAttr = sectionElement.Attribute("visible");
                if (visibleAttr != null && bool.TryParse(visibleAttr.Value, out bool parsed))
                    isVisible = parsed;

                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Iterate through all annotations on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // TextAnnotation and PopupAnnotation expose the Open property
                        if (annotation is TextAnnotation textAnn && textAnn.Name == sectionId)
                        {
                            textAnn.Open = isVisible;
                        }
                        else if (annotation is PopupAnnotation popupAnn && popupAnn.Name == sectionId)
                        {
                            popupAnn.Open = isVisible;
                        }
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Visibility settings applied and PDF saved to '{outputPath}'.");
    }
}
