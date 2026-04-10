using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations;

public static class AnnotationRectangleExtractor
{
    /// <summary>
    /// Extracts the rectangle of every annotation in the source PDF and creates a new PDF
    /// where each page visualises one rectangle (red outline) on a page of the same size.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the visualisation PDF will be saved.</param>
    public static void ExtractAnnotationRectangles(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        // Bind the source PDF with the Facade class (load operation)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // The underlying Document object gives access to pages and annotations
            Document sourceDoc = editor.Document;

            // Create a new PDF document that will hold the visualisation pages (create operation)
            using (Document visualDoc = new Document())
            {
                // Iterate through all pages of the source document
                for (int pageIndex = 1; pageIndex <= sourceDoc.Pages.Count; pageIndex++)
                {
                    Page srcPage = sourceDoc.Pages[pageIndex];

                    // Determine the size of the source page (used for the new page)
                    Aspose.Pdf.Rectangle srcPageRect = srcPage.Rect;
                    double pageWidth = srcPageRect.URX - srcPageRect.LLX;
                    double pageHeight = srcPageRect.URY - srcPageRect.LLY;

                    // Iterate through each annotation on the current page
                    foreach (Annotation ann in srcPage.Annotations)
                    {
                        // Add a new blank page with the same dimensions
                        Page newPage = visualDoc.Pages.Add();
                        newPage.SetPageSize(pageWidth, pageHeight);

                        // Create a Graph container for drawing vector shapes
                        Graph graph = new Graph(pageWidth, pageHeight);

                        // Build a rectangle shape that matches the annotation rectangle
                        // Aspose.Pdf.Drawing.Rectangle expects float parameters
                        var rectShape = new Aspose.Pdf.Drawing.Rectangle(
                            (float)ann.Rect.LLX,
                            (float)ann.Rect.LLY,
                            (float)(ann.Rect.URX - ann.Rect.LLX),
                            (float)(ann.Rect.URY - ann.Rect.LLY));

                        // Style the rectangle: red border, no fill
                        rectShape.GraphInfo = new GraphInfo
                        {
                            Color = Color.Red,
                            LineWidth = 2f,
                            FillColor = Color.Transparent
                        };

                        // Add the rectangle to the graph and the graph to the page
                        graph.Shapes.Add(rectShape);
                        newPage.Paragraphs.Add(graph);
                    }
                }

                // Save the visualisation PDF (save operation)
                visualDoc.Save(outputPdfPath);
            }

            // Close the facade (disposal handled by using)
            editor.Close();
        }
    }
}

// Minimal entry point to satisfy the compiler when building as an executable
public class Program
{
    public static void Main(string[] args)
    {
        // Expected usage: <inputPdfPath> <outputPdfPath>
        if (args.Length == 2)
        {
            AnnotationRectangleExtractor.ExtractAnnotationRectangles(args[0], args[1]);
        }
        else
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath>");
        }
    }
}