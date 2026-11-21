using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added for TextFragment and related types

public static class AnnotationExtractor
{
    /// <summary>
    /// Extracts all annotation rectangles from the input PDF and creates a new PDF where each
    /// annotation is visualised on its own page as a red rectangle (with optional type label).
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the visualisation PDF will be saved.</param>
    public static void ExtractAnnotationRectangles(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        // --------------------------------------------------------------------
        // 1. Load the source PDF and retrieve annotation information using the
        //    Document API (recommended over PdfAnnotationEditor for newer versions).
        // --------------------------------------------------------------------
        Document srcDoc = new Document(inputPdfPath);
        var annotationInfos = new List<(Aspose.Pdf.Rectangle Rect, string Type)>( );
        foreach (Page page in srcDoc.Pages)
        {
            foreach (Annotation annot in page.Annotations)
            {
                // Guard against missing rectangle (should not happen)
                if (annot.Rect == null)
                    continue;

                // Annotation.Type returns an enum; we store its name for the label.
                string typeName = annot.AnnotationType.ToString();
                annotationInfos.Add((annot.Rect, typeName));
            }
        }

        // --------------------------------------------------------------------
        // 2. Create a new PDF document that will hold the visualisation pages.
        //    The document is wrapped in a using block as required by the
        //    document-disposal-with-using rule.
        // --------------------------------------------------------------------
        using (Document outDoc = new Document())
        {
            const double margin = 20.0; // margin around the extracted rectangle

            // If there are no annotations, create an empty document with a single page
            // indicating that no annotations were found.
            if (annotationInfos.Count == 0)
            {
                Page emptyPage = outDoc.Pages.Add();
                emptyPage.PageInfo.Width = 300;
                emptyPage.PageInfo.Height = 100;
                TextFragment tf = new TextFragment("No annotations found in the source PDF.");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                tf.Position = new Position(10, 80);
                emptyPage.Paragraphs.Add(tf);
            }
            else
            {
                // ----------------------------------------------------------------
                // 3. For each annotation, create a new page that visualises the
                //    annotation rectangle.
                // ----------------------------------------------------------------
                foreach (var info in annotationInfos)
                {
                    // Create a new page
                    Page page = outDoc.Pages.Add();

                    // Compute rectangle dimensions
                    double rectWidth = info.Rect.URX - info.Rect.LLX;
                    double rectHeight = info.Rect.URY - info.Rect.LLY;

                    // Set page size (rectangle + margins + space for label)
                    page.PageInfo.Width = rectWidth + 2 * margin;
                    page.PageInfo.Height = rectHeight + 2 * margin + 30; // extra space for label

                    // Translate the original rectangle to page coordinates (origin at margin)
                    Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(
                        margin,
                        margin,
                        margin + rectWidth,
                        margin + rectHeight);

                    // ----------------------------------------------------------------
                    // 4. Add a red rectangle annotation to visualise the bounds.
                    // ----------------------------------------------------------------
                    // NOTE: Aspose.Pdf uses SquareAnnotation for rectangular shapes.
                    SquareAnnotation rectAnnot = new SquareAnnotation(page, pageRect);
                    rectAnnot.Color = Aspose.Pdf.Color.Red;                     // border color
                    rectAnnot.Border = new Border(rectAnnot) { Width = 2 };     // thicker border
                    page.Annotations.Add(rectAnnot);

                    // ----------------------------------------------------------------
                    // 5. Optionally add a text annotation above the rectangle showing the type.
                    // ----------------------------------------------------------------
                    Aspose.Pdf.Rectangle labelRect = new Aspose.Pdf.Rectangle(
                        margin,
                        margin + rectHeight + 5,
                        margin + rectWidth,
                        margin + rectHeight + 25);

                    TextAnnotation labelAnnot = new TextAnnotation(page, labelRect);
                    labelAnnot.Title = "Annotation";
                    labelAnnot.Contents = $"Type: {info.Type}";
                    labelAnnot.Color = Aspose.Pdf.Color.LightGray;
                    page.Annotations.Add(labelAnnot);
                }
            }

            // --------------------------------------------------------------------
            // 6. Save the resulting PDF.
            // --------------------------------------------------------------------
            outDoc.Save(outputPdfPath);
        }
    }

    // ------------------------------------------------------------------------
    // Entry point required for a console‑application build. It simply forwards
    // the command‑line arguments to the extractor method.
    // ------------------------------------------------------------------------
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: AnnotationExtractor <inputPdfPath> <outputPdfPath>");
            return;
        }

        try
        {
            ExtractAnnotationRectangles(args[0], args[1]);
            Console.WriteLine($"Annotation visualisation PDF saved to: {args[1]}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}