using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XML with visibility settings, and the output PDF.
        const string pdfPath = "source.pdf";
        const string xmlPath = "visibility.xml";
        const string outPdfPath = "output.pdf";

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

        // Load the XML file that defines visibility for each annotation (or logical section).
        // Expected format:
        // <Sections>
        //   <Section id="0" visible="true" />
        //   <Section id="1" visible="false" />
        //   ...
        // </Sections>
        XDocument xmlDoc = XDocument.Load(xmlPath);
        if (xmlDoc.Root == null)
        {
            Console.Error.WriteLine("Invalid XML: missing root element.");
            return;
        }

        var visibilityMap = xmlDoc.Root
                                   .Elements("Section")
                                   .Select(e => new
                                   {
                                       Id = (int?)e.Attribute("id") ?? -1,
                                       Visible = (bool?)e.Attribute("visible") ?? false
                                   })
                                   .Where(v => v.Id >= 0)
                                   .ToDictionary(v => v.Id, v => v.Visible);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over all pages.
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over all annotations on the page.
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Use a deterministic global index for the annotation.
                    int globalIndex = GetGlobalAnnotationIndex(pdfDoc, pageIndex, annIndex);
                    if (visibilityMap.TryGetValue(globalIndex, out bool visible))
                    {
                        // Aspose.Pdf does not expose an IsHidden property on the base Annotation class.
                        // Visibility is controlled via the annotation's Flags. The Hidden flag (bit 2) hides the annotation.
                        // We add or remove the Hidden flag based on the XML setting.
                        if (ann != null)
                        {
                            if (visible)
                            {
                                // Ensure the Hidden flag is cleared.
                                ann.Flags &= ~AnnotationFlags.Hidden;
                            }
                            else
                            {
                                // Set the Hidden flag.
                                ann.Flags |= AnnotationFlags.Hidden;
                            }
                        }
                    }
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outPdfPath);
        }

        Console.WriteLine($"PDF saved with updated visibility: {outPdfPath}");
    }

    /// <summary>
    /// Calculates a deterministic global index for an annotation based on its page and position.
    /// This simple implementation counts annotations sequentially across pages.
    /// </summary>
    private static int GetGlobalAnnotationIndex(Document doc, int pageNumber, int annotationNumberOnPage)
    {
        int index = 0;
        for (int i = 1; i < pageNumber; i++)
        {
            index += doc.Pages[i].Annotations.Count;
        }
        // annotationNumberOnPage is 1‑based, convert to 0‑based offset.
        index += (annotationNumberOnPage - 1);
        return index;
    }
}
