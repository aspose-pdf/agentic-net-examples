using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string xmlPath = "visibility.xml";    // XML with visibility settings
        const string outputPath = "output.pdf";      // result PDF

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

        // Load the XML that defines the default visibility for annotations.
        // Expected format:
        // <VisibilitySettings>
        //     <DefaultVisibility>true</DefaultVisibility>
        // </VisibilitySettings>
        XDocument xmlDoc = XDocument.Load(xmlPath);
        bool defaultVisibility = false;
        XElement defVisElem = xmlDoc.Root?.Element("DefaultVisibility");
        if (defVisElem != null && bool.TryParse(defVisElem.Value, out bool parsed))
            defaultVisibility = parsed;

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over all pages and their annotations.
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    // Aspose.Pdf does not expose 3‑D specific classes (Pdf3DAnnotation, Pdf3DView, etc.).
                    // Instead, we adjust the generic annotation visibility using the Flags property.
                    // If defaultVisibility is true we make sure the annotation is visible; otherwise we hide it.
                    if (defaultVisibility)
                    {
                        // Remove the Invisible flag if it is set.
                        ann.Flags &= ~AnnotationFlags.Invisible;
                    }
                    else
                    {
                        // Add the Invisible flag to hide the annotation.
                        ann.Flags |= AnnotationFlags.Invisible;
                    }
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated visibility to '{outputPath}'.");
    }
}
