using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

/// <summary>
/// Demonstrates loading a PDF, applying several edits, and saving the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a text annotation.
            AddTextAnnotation(doc);

            // Add a hyperlink annotation.
            AddLinkAnnotation(doc);

            // Draw a rectangle shape on the first page.
            AddRectangleShape(doc);

            // Apply an image stamp to all pages.
            AddImageStamp(doc);

            // Save the edited PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Adds a text annotation to the first page.
    /// </summary>
    /// <remarks>
    /// <para>Fixes applied:</para>
    /// <list type="bullet">
    ///   <item><description>Qualified the ambiguous <c>Rectangle</c> type with <c>Aspose.Pdf.Rectangle</c> (annotation bounds).</description></item>
    ///   <item><description>Qualified the ambiguous <c>Color</c> type with <c>Aspose.Pdf.Color</c>.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="doc">The PDF document.</param>
    static void AddTextAnnotation(Document doc)
    {
        Page page = doc.Pages[1];
        // Annotation rectangle (llx, lly, urx, ury).
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
        TextAnnotation textAnn = new TextAnnotation(page, rect)
        {
            Title = "Note",
            Contents = "This is a text annotation.",
            Color = Aspose.Pdf.Color.Yellow,
            Open = true,
            Icon = TextIcon.Note
        };
        page.Annotations.Add(textAnn);
    }

    /// <summary>
    /// Adds a hyperlink annotation to the first page.
    /// </summary>
    /// <remarks>
    /// <para>Fixes applied:</para>
    /// <list type="bullet">
    ///   <item><description>Qualified the ambiguous <c>Rectangle</c> type with <c>Aspose.Pdf.Rectangle</c>.</description></item>
    ///   <item><description>Qualified the ambiguous <c>Color</c> type with <c>Aspose.Pdf.Color</c>.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="doc">The PDF document.</param>
    static void AddLinkAnnotation(Document doc)
    {
        Page page = doc.Pages[1];
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(320, 500, 520, 550);
        LinkAnnotation linkAnn = new LinkAnnotation(page, rect)
        {
            Action = new GoToURIAction("https://www.example.com"),
            Color = Aspose.Pdf.Color.Blue
        };
        page.Annotations.Add(linkAnn);
    }

    /// <summary>
    /// Draws a light‑gray rectangle shape on the first page using a <c>Graph</c>.
    /// </summary>
    /// <remarks>
    /// <para>Fixes applied:</para>
    /// <list type="bullet">
    ///   <item><description>Used <c>Graph(double,double)</c> constructor (double literals) to avoid deprecation warnings.</description></item>
    ///   <item><description>Used fully‑qualified <c>Aspose.Pdf.Drawing.Rectangle</c> for the drawing shape.</description></item>
    ///   <item><description>Qualified <c>Color</c> references with <c>Aspose.Pdf.Color</c>.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="doc">The PDF document.</param>
    static void AddRectangleShape(Document doc)
    {
        Page page = doc.Pages[1];
        // Graph constructor expects double values.
        Graph graph = new Graph(400.0, 200.0);

        // Drawing rectangle expects float parameters.
        Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
        shapeRect.GraphInfo = new GraphInfo
        {
            FillColor = Aspose.Pdf.Color.LightGray,
            Color = Aspose.Pdf.Color.Black,
            LineWidth = 1f
        };
        graph.Shapes.Add(shapeRect);
        page.Paragraphs.Add(graph);
    }

    /// <summary>
    /// Applies an image stamp to every page of the document.
    /// </summary>
    /// <param name="doc">The PDF document.</param>
    static void AddImageStamp(Document doc)
    {
        const string stampImagePath = "stamp.png";
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        ImageStamp imgStamp = new ImageStamp(stampImagePath)
        {
            Background = false,
            Opacity = 0.5f,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        foreach (Page page in doc.Pages)
        {
            page.AddStamp(imgStamp);
        }
    }
}
