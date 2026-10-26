using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExportPopupAnnotations
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "popup_annotations.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare an empty XFDF structure
            XDocument xfdfDoc = new XDocument(
                new XElement("xfdf",
                    new XElement("annots")
                )
            );

            XElement annotsElement = xfdfDoc.Root.Element("annots");

            // Iterate through all pages and collect only Popup annotations
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is PopupAnnotation popup)
                    {
                        // Build a minimal XFDF representation for the popup annotation
                        // The XFDF spec allows attributes such as page, rect, contents, etc.
                        var rect = popup.Rect; // Rectangle: LLX, LLY, URX, URY
                        string rectValue = $"{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}";

                        XElement popupElement = new XElement("popup",
                            new XAttribute("page", page.Number),
                            new XAttribute("rect", rectValue),
                            new XAttribute("contents", popup.Contents ?? string.Empty)
                        );

                        annotsElement.Add(popupElement);
                    }
                }
            }

            // Save the filtered XFDF containing only popup annotations
            xfdfDoc.Save(outputXfdfPath);
        }

        Console.WriteLine($"Popup annotations exported to: {outputXfdfPath}");
    }
}
