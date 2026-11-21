using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class InteractionReplayer
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string xmlLogPath = "interaction_log.xml";
        const string outputPdf = "replayed.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlLogPath))
        {
            Console.Error.WriteLine($"XML log not found: {xmlLogPath}");
            return;
        }

        // Load interaction log (expected format: <Log><Action type="..." .../></Log>)
        XDocument logDoc = XDocument.Load(xmlLogPath);

        using (Document pdfDoc = new Document(pdfPath))
        {
            foreach (XElement action in logDoc.Root.Elements("Action"))
            {
                string type = (string)action.Attribute("type");
                switch (type)
                {
                    case "GoToPage":
                        int pageNum = (int)action.Attribute("page");
                        if (pageNum >= 1 && pageNum <= pdfDoc.Pages.Count)
                        {
                            Page page = pdfDoc.Pages[pageNum];
                            TextFragment tf = new TextFragment($"[Visited page {pageNum}]");
                            tf.TextState.FontSize = 12;
                            tf.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.2, 0.2);
                            tf.Position = new Position(50, page.PageInfo.Height - 50);
                            page.Paragraphs.Add(tf);
                        }
                        break;

                    case "AddTextAnnotation":
                        int annPage = (int)action.Attribute("page");
                        double x = (double)action.Attribute("x");
                        double y = (double)action.Attribute("y");
                        string text = (string)action.Attribute("text");
                        if (annPage >= 1 && annPage <= pdfDoc.Pages.Count)
                        {
                            Page page = pdfDoc.Pages[annPage];
                            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + 200, y + 50);
                            TextAnnotation txtAnn = new TextAnnotation(page, rect);
                            txtAnn.Title = "Log";
                            txtAnn.Contents = text;
                            txtAnn.Open = true;
                            txtAnn.Icon = TextIcon.Note;
                            page.Annotations.Add(txtAnn);
                        }
                        break;

                    case "Zoom":
                        int zoomPage = (int)action.Attribute("page");
                        double factor = (double)action.Attribute("factor");
                        if (zoomPage >= 1 && zoomPage <= pdfDoc.Pages.Count)
                        {
                            Page page = pdfDoc.Pages[zoomPage];
                            TextFragment zoomTf = new TextFragment($"[Zoom factor: {factor}]");
                            zoomTf.TextState.FontSize = 10;
                            zoomTf.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.2, 0.2, 0.8);
                            zoomTf.Position = new Position(50, page.PageInfo.Height - 70);
                            page.Paragraphs.Add(zoomTf);
                        }
                        break;

                    // Additional action types can be handled here
                }
            }

            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Replayed interactions saved to '{outputPdf}'.");
    }
}