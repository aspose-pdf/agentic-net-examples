using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

public static class AnnotationRectangleExtractor
{
    /// <summary>
    /// Extracts all annotation rectangles from the input PDF and creates a new PDF where each
    /// annotation is visualized on its own page as a red rectangle.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the visual‑inspection PDF will be saved.</param>
    public static void ExtractAnnotationRectangles(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        // Bind the source PDF with PdfAnnotationEditor (facade API)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Get total page count (1‑based indexing)
            int pageCount = editor.Document.Pages.Count;

            // Retrieve all annotation types supported by the enum
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract every annotation from the whole document
            IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, allTypes);

            // Create a new PDF document that will hold the visual pages
            using (Document visualDoc = new Document())
            {
                // Iterate over each annotation and create a page showing its rectangle
                foreach (Annotation annot in annotations)
                {
                    // The rectangle of the annotation (coordinates are in PDF user space)
                    Aspose.Pdf.Rectangle annotRect = annot.Rect;

                    // Create a new page with the same size as the source page that contains the annotation
                    // (fallback to A4 size if the source page size cannot be determined)
                    Page sourcePage = editor.Document.Pages[annot.PageIndex];
                    Page newPage = visualDoc.Pages.Add();
                    newPage.SetPageSize(sourcePage.MediaBox.Width, sourcePage.MediaBox.Height);

                    // Prepare a Graph container (drawing surface)
                    // Width and height can be the page size; the rectangle will be placed using absolute coordinates
                    Graph graph = new Graph(newPage.MediaBox.Width, newPage.MediaBox.Height);

                    // Create a drawing rectangle that matches the annotation rectangle
                    // Constructor expects float values
                    var shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                        (float)annotRect.LLX,
                        (float)annotRect.LLY,
                        (float)annotRect.Width,
                        (float)annotRect.Height);

                    // Style the rectangle (red border, no fill)
                    shapeRect.GraphInfo = new GraphInfo
                    {
                        Color = Aspose.Pdf.Color.Red,
                        LineWidth = 2f,
                        FillColor = Aspose.Pdf.Color.Transparent
                    };

                    // Add the shape to the graph and the graph to the page
                    graph.Shapes.Add(shapeRect);
                    newPage.Paragraphs.Add(graph);
                }

                // Save the visual inspection PDF
                visualDoc.Save(outputPdfPath);
            }

            // Close the editor (optional, handled by using)
            editor.Close();
        }
    }
}

public class Program
{
    // Entry point required for a console application
    public static void Main(string[] args)
    {
        // Simple argument handling – if none supplied, use placeholder file names.
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputPath = args.Length > 1 ? args[1] : "annotations_visual.pdf";

        try
        {
            AnnotationRectangleExtractor.ExtractAnnotationRectangles(inputPath, outputPath);
            Console.WriteLine($"Annotation visual PDF created at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
