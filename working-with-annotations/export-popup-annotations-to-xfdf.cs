using System;
using System.IO;
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

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the root XFDF structure.
            XDocument xfdfDoc = new XDocument(
                new XElement("xfdf",
                    new XAttribute("xml:space", "preserve"),
                    new XElement("annots") // container for annotation elements
                )
            );

            XElement annotsElement = xfdfDoc.Root.Element("annots");

            // Iterate over all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over annotations on the current page.
                foreach (Annotation annotation in page.Annotations)
                {
                    // Process only Popup annotations.
                    if (annotation is PopupAnnotation popup)
                    {
                        // Build a <popup> element with relevant attributes.
                        // Rectangle coordinates are space‑separated as per XFDF spec.
                        Aspose.Pdf.Rectangle rect = popup.Rect;
                        string rectValue = $"{rect.LLX} {rect.LLY} {rect.URX} {rect.URY}";

                        XElement popupElement = new XElement("popup",
                            new XAttribute("page", pageIndex),
                            new XAttribute("rect", rectValue)
                        );

                        // Optional properties: Title, Contents, Open state.
                        // The Title is stored on the parent markup annotation (if any).
                        if (popup.Parent is MarkupAnnotation parentMarkup && !string.IsNullOrEmpty(parentMarkup.Title))
                        {
                            popupElement.Add(new XAttribute("title", parentMarkup.Title));
                        }

                        if (!string.IsNullOrEmpty(popup.Contents))
                        {
                            popupElement.Add(new XAttribute("contents", popup.Contents));
                        }

                        // The Open property indicates whether the popup is displayed.
                        popupElement.Add(new XAttribute("open", popup.Open ? "true" : "false"));

                        // Append the popup element to the XFDF document.
                        annotsElement.Add(popupElement);
                    }
                }
            }

            // Save the constructed XFDF XML to the specified file.
            xfdfDoc.Save(outputXfdfPath);
            Console.WriteLine($"Popup annotations exported to '{outputXfdfPath}'.");
        }
    }
}
