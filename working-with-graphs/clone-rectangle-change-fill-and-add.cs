using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class PdfHelper
{
    // Clones a drawing rectangle, changes its fill color, and adds the clone to the provided graph.
    public static void CloneRectangleChangeFillAndAdd(
        Graph graph,
        Aspose.Pdf.Drawing.Rectangle originalRect,
        Aspose.Pdf.Color newFillColor)
    {
        if (graph == null) throw new ArgumentNullException(nameof(graph));
        if (originalRect == null) throw new ArgumentNullException(nameof(originalRect));
        if (newFillColor == null) throw new ArgumentNullException(nameof(newFillColor));

        // Perform a manual deep clone of the original rectangle because Aspose.Pdf.Drawing.Rectangle does not implement ICloneable.
        // The constructor takes (left, bottom, width, height) as floats.
        var clonedRect = new Aspose.Pdf.Drawing.Rectangle(
            (float)originalRect.Left,
            (float)originalRect.Bottom,
            (float)originalRect.Width,
            (float)originalRect.Height);

        // Preserve other visual settings from the original rectangle (optional but often desired).
        // GraphInfo is a reference type; create a new instance and copy the needed properties.
        clonedRect.GraphInfo = new GraphInfo
        {
            // Keep the original border color and width if they exist.
            Color = originalRect.GraphInfo?.Color,
            LineWidth = originalRect.GraphInfo?.LineWidth ?? 0,
            // Apply the new fill color as requested.
            FillColor = newFillColor
        };

        // Add the cloned rectangle to the graph's shape collection.
        graph.Shapes.Add(clonedRect);
    }
}

// Minimal entry point required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left empty – the library method can be invoked from other code.
    }
}