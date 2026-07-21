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
    /// Extracts all annotation rectangles from the input PDF and creates a new PDF where each page
    /// contains a visual representation (red rectangle) of one annotation.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the inspection PDF will be saved.</param>
    public static void ExtractAnnotationRectangles(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        // Facade to work with annotations
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdfPath);

        // Create a new PDF document that will hold the visual pages
        using (Document outDoc = new Document())
        {
            // Iterate through all pages of the source document
            int pageCount = editor.Document.Pages.Count;
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                Page srcPage = editor.Document.Pages[pageIndex];
                // Iterate through each annotation on the current page
                foreach (Annotation ann in srcPage.Annotations)
                {
                    // Get the annotation rectangle (position and size)
                    Aspose.Pdf.Rectangle annRect = ann.Rect;

                    // Create a new blank page with the same size as the source page
                    Page newPage = outDoc.Pages.Add();
                    newPage.MediaBox = new Aspose.Pdf.Rectangle(
                        srcPage.MediaBox.LLX,
                        srcPage.MediaBox.LLY,
                        srcPage.MediaBox.URX,
                        srcPage.MediaBox.URY);

                    // Create a Graph container matching the page size (width, height as double)
                    Graph graph = new Graph((double)newPage.MediaBox.Width, (double)newPage.MediaBox.Height);

                    // Create a drawing rectangle that matches the annotation rectangle
                    // Constructor: left, bottom, width, height (all float)
                    var drawRect = new Aspose.Pdf.Drawing.Rectangle(
                        (float)annRect.LLX,
                        (float)annRect.LLY,
                        (float)(annRect.URX - annRect.LLX),
                        (float)(annRect.URY - annRect.LLY));

                    // Style the rectangle (red border, no fill)
                    drawRect.GraphInfo = new GraphInfo
                    {
                        Color = Aspose.Pdf.Color.Red,
                        LineWidth = 2f,
                        FillColor = Aspose.Pdf.Color.Transparent
                    };

                    // Add the rectangle shape to the graph and the graph to the page
                    graph.Shapes.Add(drawRect);
                    newPage.Paragraphs.Add(graph);
                }
            }

            // Save the inspection PDF
            outDoc.Save(outputPdfPath);
        }

        // Release resources held by the facade
        editor.Close();
    }
}

// Simple entry point so the project builds as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // If arguments are supplied, use them; otherwise demonstrate with placeholder paths.
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