using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string xmlPath = "visibility.xml";    // XML with visibility settings
        const string outputPath = "output.pdf";     // result PDF

        // Verify input files exist
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

        // Load the XML file and build a map: section identifier -> visibility flag
        // Expected XML format:
        // <VisibilitySettings>
        //   <Section id="1" visible="true" />
        //   <Section id="2" visible="false" />
        //   ...
        // </VisibilitySettings>
        XDocument xmlDoc = XDocument.Load(xmlPath);
        XElement? root = xmlDoc.Root;
        if (root == null)
        {
            Console.Error.WriteLine("Invalid XML: missing root element.");
            return;
        }

        // Build a safe dictionary, ignoring malformed entries.
        Dictionary<int, bool> visibilityMap = root
            .Elements("Section")
            .Select(e => new
            {
                Id = (int?)e.Attribute("id"),
                Visible = (bool?)e.Attribute("visible")
            })
            .Where(x => x.Id.HasValue && x.Visible.HasValue)
            .ToDictionary(x => x.Id!.Value, x => x.Visible!.Value);

        // Open the PDF document (using block ensures proper disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate through all pages
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate through all annotations on the page
                // The index of the annotation (1‑based) is used as the section identifier.
                for (int i = 0; i < page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    int sectionId = i + 1; // 1‑based identifier to match XML ids

                    if (visibilityMap.TryGetValue(sectionId, out bool visible))
                    {
                        // Aspose.PDF controls visibility via the Hidden flag in Annotation.Flags.
                        // When the flag is set, the annotation is not shown.
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

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated visibility states to '{outputPath}'.");
    }
}
