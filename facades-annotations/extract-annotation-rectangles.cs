using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // <-- added for TextFragment and Position

public static class AnnotationRectangleExtractor
{
    /// <summary>
    /// Extracts the rectangle of every annotation in the source PDF and creates a new PDF
    /// where each page visualises one annotation rectangle.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the visualisation PDF will be saved.</param>
    public static void ExtractAnnotationRectangles(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF not found.", inputPdfPath);

        // Bind the source PDF with the facade (PdfAnnotationEditor is a Facade class, not IDisposable)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdfPath);

        // The underlying Document object gives access to pages and their annotations.
        Document srcDoc = editor.Document;

        // Prepare the output document.
        using (Document outDoc = new Document())
        {
            // Iterate through all pages of the source document.
            for (int pageIdx = 1; pageIdx <= srcDoc.Pages.Count; pageIdx++)
            {
                Page srcPage = srcDoc.Pages[pageIdx];
                // Iterate over each annotation on the current page.
                foreach (Annotation annot in srcPage.Annotations)
                {
                    // Create a new blank page in the output document.
                    Page outPage = outDoc.Pages.Add();

                    // Copy the size of the source page so the rectangle appears in the same coordinate space.
                    outPage.MediaBox = srcPage.MediaBox;
                    outPage.CropBox = srcPage.CropBox;
                    outPage.TrimBox = srcPage.TrimBox;
                    outPage.ArtBox = srcPage.ArtBox;
                    outPage.BleedBox = srcPage.BleedBox;

                    // The annotation rectangle (in page coordinates).
                    Aspose.Pdf.Rectangle annotRect = annot.Rect;

                    // Draw the rectangle using the Drawing API.
                    // Graph constructor takes width and height of the drawing canvas (double).
                    double pageWidth = outPage.MediaBox.URX - outPage.MediaBox.LLX;
                    double pageHeight = outPage.MediaBox.URY - outPage.MediaBox.LLY;
                    Graph graph = new Graph(pageWidth, pageHeight);

                    // Create a rectangle shape that matches the annotation bounds.
                    // Drawing.Rectangle constructor expects float values.
                    float rectX = (float)annotRect.LLX;
                    float rectY = (float)annotRect.LLY;
                    float rectWidth = (float)(annotRect.URX - annotRect.LLX);
                    float rectHeight = (float)(annotRect.URY - annotRect.LLY);
                    Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                        rectX,
                        rectY,
                        rectWidth,
                        rectHeight);

                    // Visual styling via GraphInfo.
                    shapeRect.GraphInfo = new GraphInfo
                    {
                        Color = Aspose.Pdf.Color.Red,          // border color
                        LineWidth = 2f,                         // border thickness (float)
                        FillColor = Aspose.Pdf.Color.FromRgb(1f, 1f, 0.8f) // light yellow fill
                    };

                    graph.Shapes.Add(shapeRect);
                    outPage.Paragraphs.Add(graph);

                    // Optional: add a text annotation describing the original annotation type.
                    string info = $"Page {pageIdx}, Type: {annot.AnnotationType}";
                    TextFragment tf = new TextFragment(info)
                    {
                        // Position the text near the top‑left corner of the page.
                        Position = new Position(10, outPage.MediaBox.URY - 20),
                        TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                    };
                    outPage.Paragraphs.Add(tf);
                }
            }

            // Save the visualisation PDF.
            outDoc.Save(outputPdfPath);
        }

        // Close the facade (releases the bound document).
        editor.Close();
    }
}

public class Program
{
    /// <summary>
    /// Simple entry point required for compilation. Expects two arguments: input PDF path and output PDF path.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AnnotationRectangleExtractor <inputPdfPath> <outputPdfPath>");
            return;
        }

        AnnotationRectangleExtractor.ExtractAnnotationRectangles(args[0], args[1]);
    }
}
