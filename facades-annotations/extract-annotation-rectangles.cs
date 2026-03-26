using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;

namespace ExtractAnnotationRectanglesExample
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "annotations_visual.pdf";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Bind the source PDF to the annotation editor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                Document sourceDoc = editor.Document;
                int pageCount = sourceDoc.Pages.Count;

                // Define annotation types to extract (common types)
                string[] annotTypes = new string[] {
                    "Text", "Highlight", "Square", "Circle", "Link", "FreeText",
                    "Line", "Stamp", "Ink", "Popup", "FileAttachment", "Sound",
                    "Movie", "Screen", "Widget", "PrinterMark", "Watermark", "3D", "RichMedia"
                };

                IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, annotTypes);

                // Create a new PDF to hold visual pages
                using (Document outDoc = new Document())
                {
                    foreach (Annotation ann in annotations)
                    {
                        // Get the rectangle of the annotation (in page coordinates)
                        Aspose.Pdf.Rectangle annRect = ann.Rect;

                        // Create a new blank page (size 600x800 points)
                        Page page = outDoc.Pages.Add();
                        page.PageInfo.Width = 600;
                        page.PageInfo.Height = 800;

                        // Draw the annotation rectangle on the page using a Graph container
                        Graph graph = new Graph(600.0, 800.0); // double literals as required by the new constructor
                        var shape = new Aspose.Pdf.Drawing.Rectangle(
                            (float)annRect.LLX,
                            (float)annRect.LLY,
                            (float)(annRect.URX - annRect.LLX),
                            (float)(annRect.URY - annRect.LLY));
                        shape.GraphInfo = new GraphInfo
                        {
                            Color = Aspose.Pdf.Color.Red,
                            LineWidth = 2f // float literal
                        };
                        graph.Shapes.Add(shape);
                        page.Paragraphs.Add(graph);
                    }

                    outDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Annotation rectangles visualized in '{outputPath}'.");
        }
    }
}
