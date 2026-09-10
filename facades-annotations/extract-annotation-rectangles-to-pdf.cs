using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public static class AnnotationRectangleExtractor
{
    /// <summary>
    /// Extracts the rectangle of each annotation in the source PDF and creates a new PDF
    /// where each page visualises one annotation rectangle.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the visualisation PDF will be saved.</param>
    public static void ExtractAnnotationRectangles(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        // Bind the source PDF with the annotation editor facade.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Determine the page range to scan.
            int startPage = 1;
            int endPage   = editor.Document.Pages.Count;

            // Request all common annotation types.
            string[] annotTypes = new string[]
            {
                "Text", "Highlight", "Square", "Circle", "Line", "FreeText",
                "Link", "Ink", "Stamp", "FileAttachment", "Sound", "Movie",
                "RubberStamp", "Popup", "Caret", "Polygon", "PolyLine", "SquareCircle"
            };

            // Extract annotations from the specified range.
            IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, annotTypes);

            // Create a new PDF document that will hold the visualisation pages.
            using (Document visualDoc = new Document())
            {
                int pageIndex = 1;
                foreach (Annotation annot in annotations)
                {
                    // Get the rectangle of the annotation (in user space coordinates).
                    Aspose.Pdf.Rectangle annotRect = annot.Rect;

                    // Create a fresh page for this annotation.
                    Page page = visualDoc.Pages.Add();

                    // Optional: add a text fragment describing the annotation.
                    TextFragment tf = new TextFragment(
                        $"Annotation {pageIndex}: Type={annot.AnnotationType}, Page={annot.PageIndex}");
                    tf.Position = new Position(10, page.Rect.Height - 20);
                    tf.TextState.FontSize = 12;
                    tf.TextState.ForegroundColor = Color.Black;
                    page.Paragraphs.Add(tf);

                    // Draw the annotation rectangle using a Graph.
                    // The Graph size is set to the page size.
                    Graph graph = new Graph(page.Rect.Width, page.Rect.Height);

                    // Create a rectangle shape that matches the annotation bounds.
                    // The drawing rectangle constructor expects (left, bottom, width, height) as floats.
                    var shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)annotRect.LLX,
                        (float)annotRect.LLY,
                        (float)(annotRect.URX - annotRect.LLX),
                        (float)(annotRect.URY - annotRect.LLY));

                    // Style the rectangle for clear visual inspection.
                    shape.GraphInfo = new GraphInfo
                    {
                        Color = Color.Red,               // stroke color
                        FillColor = Color.Transparent,   // no fill
                        LineWidth = 2f                   // float value
                    };

                    graph.Shapes.Add(shape);
                    page.Paragraphs.Add(graph);

                    pageIndex++;
                }

                // Save the visualisation PDF.
                visualDoc.Save(outputPdfPath);
            }
        }
    }

    // Simple entry point to make the project compile when built as an executable.
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: AnnotationRectangleExtractor <input-pdf> <output-pdf>");
            return;
        }

        try
        {
            ExtractAnnotationRectangles(args[0], args[1]);
            Console.WriteLine($"Annotation visualisation saved to: {args[1]}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
