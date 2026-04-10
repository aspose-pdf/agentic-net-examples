using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "visibility.xml";
        const string outputPath = "output.pdf";

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

        // Load visibility settings from XML.
        // Expected format:
        // <Visibility>
        //   <Section Index="0" Visible="true" />
        //   <Section Index="1" Visible="false" />
        //   ...
        // </Visibility>
        var visibilityMap = new Dictionary<int, bool>();
        try
        {
            XDocument xDoc = XDocument.Load(xmlPath);
            if (xDoc.Root == null)
            {
                Console.Error.WriteLine("XML does not contain a root element.");
                return;
            }
            foreach (var elem in xDoc.Root.Elements("Section"))
            {
                XAttribute indexAttr = elem.Attribute("Index");
                XAttribute visibleAttr = elem.Attribute("Visible");
                if (indexAttr == null || visibleAttr == null)
                    continue; // skip malformed entries

                if (!int.TryParse(indexAttr.Value, out int index))
                    continue;
                if (!bool.TryParse(visibleAttr.Value, out bool visible))
                    continue;

                visibilityMap[index] = visible;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read XML: {ex.Message}");
            return;
        }

        // Open the PDF, apply visibility states, and save.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate all pages.
            foreach (Page page in pdfDoc.Pages)
            {
                int annotationIndex = 0;
                // Iterate all annotations on the page.
                foreach (Annotation annotation in page.Annotations)
                {
                    // Apply visibility if a setting exists for this index.
                    if (visibilityMap.TryGetValue(annotationIndex, out bool isVisible))
                    {
                        if (isVisible)
                        {
                            // Ensure the Hidden flag is cleared.
                            annotation.Flags &= ~AnnotationFlags.Hidden;
                        }
                        else
                        {
                            // Set the Hidden flag to hide the annotation.
                            annotation.Flags |= AnnotationFlags.Hidden;
                        }
                    }
                    annotationIndex++;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated visibility to '{outputPath}'.");
    }
}
