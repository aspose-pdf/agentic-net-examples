using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "target_with_annotations.pdf";

        // Export annotations from source PDF to XFDF
        ExportAnnotationsToXfdf(sourcePdfPath, xfdfPath);

        // Import XFDF annotations into target PDF
        ImportXfdfToPdf(targetPdfPath, xfdfPath, outputPdfPath);
    }

    // Exports all TextAnnotations from a PDF to a simple XFDF file
    static void ExportAnnotationsToXfdf(string pdfPath, string xfdfOutputPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        using (Aspose.Pdf.Document srcDoc = new Aspose.Pdf.Document(pdfPath))
        {
            using (XmlWriter writer = XmlWriter.Create(xfdfOutputPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("xfdf", "http://ns.adobe.com/xfdf/");
                writer.WriteStartElement("annots");

                // Iterate pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
                {
                    Aspose.Pdf.Page page = srcDoc.Pages[pageNum];
                    foreach (Aspose.Pdf.Annotations.Annotation annot in page.Annotations)
                    {
                        // Handle only TextAnnotation for this example
                        if (annot is Aspose.Pdf.Annotations.TextAnnotation textAnnot)
                        {
                            Aspose.Pdf.Rectangle rect = textAnnot.Rect;

                            writer.WriteStartElement("text");
                            writer.WriteAttributeString("page", pageNum.ToString());
                            writer.WriteAttributeString("rect",
                                $"{rect.LLX} {rect.LLY} {rect.URX} {rect.URY}");
                            writer.WriteAttributeString("contents", textAnnot.Contents ?? string.Empty);
                            writer.WriteEndElement(); // text
                        }
                    }
                }

                writer.WriteEndElement(); // annots
                writer.WriteEndElement(); // xfdf
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Annotations exported to XFDF: {xfdfOutputPath}");
    }

    // Imports TextAnnotations from an XFDF file into a PDF
    static void ImportXfdfToPdf(string pdfPath, string xfdfPath, string outputPdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        using (Aspose.Pdf.Document targetDoc = new Aspose.Pdf.Document(pdfPath))
        {
            XmlDocument xfdfDoc = new XmlDocument();
            xfdfDoc.Load(xfdfPath);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xfdfDoc.NameTable);
            nsmgr.AddNamespace("xf", "http://ns.adobe.com/xfdf/");

            XmlNodeList textNodes = xfdfDoc.SelectNodes("//xf:annots/xf:text", nsmgr);
            foreach (XmlNode node in textNodes)
            {
                // Parse required attributes
                int pageNumber = int.Parse(node.Attributes["page"].Value);
                string[] rectParts = node.Attributes["rect"].Value.Split(' ');
                if (rectParts.Length != 4) continue;

                double llx = double.Parse(rectParts[0]);
                double lly = double.Parse(rectParts[1]);
                double urx = double.Parse(rectParts[2]);
                double ury = double.Parse(rectParts[3]);

                string contents = node.Attributes["contents"]?.Value ?? string.Empty;

                // Ensure page exists
                if (pageNumber < 1 || pageNumber > targetDoc.Pages.Count) continue;

                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                Aspose.Pdf.Annotations.TextAnnotation newAnnot = new Aspose.Pdf.Annotations.TextAnnotation(targetDoc.Pages[pageNumber], rect)
                {
                    Contents = contents,
                    Title    = "Imported",
                    Open     = false,
                    Icon     = Aspose.Pdf.Annotations.TextIcon.Note
                };

                targetDoc.Pages[pageNumber].Annotations.Add(newAnnot);
            }

            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and saved to: {outputPdfPath}");
    }
}