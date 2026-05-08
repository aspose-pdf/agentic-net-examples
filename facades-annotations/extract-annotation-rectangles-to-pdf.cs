using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

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

        // Bind the source PDF with the PdfAnnotationEditor facade.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // The underlying Document instance.
            Document srcDoc = editor.Document;

            // Create a new empty PDF that will hold the visualisation pages.
            using (Document outDoc = new Document())
            {
                // Iterate through all pages of the source document.
                for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
                {
                    Page srcPage = srcDoc.Pages[pageIndex];

                    // Iterate through all annotations on the current page.
                    foreach (Annotation ann in srcPage.Annotations)
                    {
                        // Create a new page in the output document for this annotation.
                        Page outPage = outDoc.Pages.Add();

                        // Preserve the original page size for accurate visualisation.
                        outPage.SetPageSize(srcPage.Rect.Width, srcPage.Rect.Height);

                        // Create a Graph container that matches the page size.
                        Graph graph = new Graph(srcPage.Rect.Width, srcPage.Rect.Height);

                        // Build a drawing rectangle that matches the annotation rectangle.
                        // Aspose.Pdf.Drawing.Rectangle constructor expects float values.
                        Aspose.Pdf.Drawing.Rectangle drawRect = new Aspose.Pdf.Drawing.Rectangle(
                            (float)ann.Rect.LLX,
                            (float)ann.Rect.LLY,
                            (float)(ann.Rect.URX - ann.Rect.LLX),
                            (float)(ann.Rect.URY - ann.Rect.LLY));

                        // Style the rectangle (red border, no fill).
                        drawRect.GraphInfo = new GraphInfo
                        {
                            Color = Aspose.Pdf.Color.Red,
                            LineWidth = 2f, // float literal
                            FillColor = Aspose.Pdf.Color.Transparent
                        };

                        // Add the rectangle shape to the graph and the graph to the page.
                        graph.Shapes.Add(drawRect);
                        outPage.Paragraphs.Add(graph);
                    }
                }

                // Save the visualisation PDF.
                outDoc.Save(outputPdfPath);
            }

            // Close the facade (optional, as using will dispose it).
            editor.Close();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AnnotationRectangleExtractor <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            AnnotationRectangleExtractor.ExtractAnnotationRectangles(inputPath, outputPath);
            Console.WriteLine("Annotation rectangles extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
