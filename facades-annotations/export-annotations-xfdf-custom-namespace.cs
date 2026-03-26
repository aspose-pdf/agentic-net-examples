using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";
        const string outputXfdf = "custom_annotations.xfdf";
        const string customNamespace = "http://custom.org/xfdf";

        // Create a simple PDF with a text annotation
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, rect);
            textAnn.Title = "Note";
            textAnn.Contents = "Sample annotation";
            page.Annotations.Add(textAnn);

            // Save the PDF (optional, needed for later loading)
            doc.Save(outputPdf);
        }

        // Export annotations to XFDF and replace the default namespace with a custom one
        using (Document doc = new Document(outputPdf))
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    editor.ExportAnnotationsToXfdf(ms);
                    ms.Position = 0;
                    XDocument xfdfDoc = XDocument.Load(ms);

                    // Replace the default XFDF namespace with the custom namespace
                    XNamespace newNs = customNamespace;
                    XDocument newXfdf = new XDocument(
                        new XElement(
                            newNs + xfdfDoc.Root.Name.LocalName,
                            // Preserve non‑namespace attributes (e.g., version)
                            xfdfDoc.Root.Attributes().Where(a => !a.IsNamespaceDeclaration),
                            // Recursively copy child elements with the new namespace
                            xfdfDoc.Root.Elements().Select(e => ChangeNamespace(e, newNs))
                        )
                    );

                    using (FileStream fileStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
                    {
                        newXfdf.Save(fileStream);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Recursively creates a copy of an element (and its descendants) using the supplied namespace.
    /// </summary>
    private static XElement ChangeNamespace(XElement element, XNamespace newNs)
    {
        return new XElement(
            newNs + element.Name.LocalName,
            // Preserve non‑namespace attributes
            element.Attributes().Where(a => !a.IsNamespaceDeclaration),
            // Recurse into children
            element.Elements().Select(e => ChangeNamespace(e, newNs))
        );
    }
}
