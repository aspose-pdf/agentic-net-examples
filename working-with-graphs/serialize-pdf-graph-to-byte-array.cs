using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class PdfGraphSerializer
{
    // Creates a PDF containing a simple graph and returns it as a byte array.
    public static byte[] CreatePdfWithGraph()
    {
        // MemoryStream will hold the PDF bytes.
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Document lifecycle must be wrapped in a using block.
            using (Document pdfDocument = new Document())
            {
                // Add a new page to the document.
                Page page = pdfDocument.Pages.Add();

                // Create a Graph container (width, height) to hold vector shapes.
                // Use the double‑based constructor as the float overload is obsolete.
                Graph graph = new Graph(400.0, 200.0);

                // Draw X‑axis.
                float[] xAxisPoints = { 50, 150, 350, 150 };
                Line xAxis = new Line(xAxisPoints);
                xAxis.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(xAxis);

                // Draw Y‑axis.
                float[] yAxisPoints = { 50, 150, 50, 50 };
                Line yAxis = new Line(yAxisPoints);
                yAxis.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(yAxis);

                // Draw a simple line chart representing sample data.
                float[] dataPoints = { 50, 150, 100, 120, 150, 130, 200, 90, 250, 140, 300, 80 };
                Line dataLine = new Line(dataPoints);
                dataLine.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 2f
                };
                graph.Shapes.Add(dataLine);

                // Add the graph to the page's paragraph collection.
                page.Paragraphs.Add(graph);

                // Save the PDF into the memory stream.
                // Guard the Save call on non‑Windows platforms where libgdiplus may be missing.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    pdfDocument.Save(outputStream);
                }
                else
                {
                    try
                    {
                        pdfDocument.Save(outputStream);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        // libgdiplus (GDI+) is not available – return an empty PDF or handle gracefully.
                        // Here we return an empty byte array to keep the method contract.
                        return new byte[0];
                    }
                }
            }

            // Return the accumulated PDF bytes.
            return outputStream.ToArray();
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus).
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}

class Program
{
    // Entry point required for a console application.
    static void Main(string[] args)
    {
        // Generate the PDF and optionally write it to disk for verification.
        byte[] pdfBytes = PdfGraphSerializer.CreatePdfWithGraph();
        // The following line is optional; it demonstrates that the byte array can be used.
        if (pdfBytes.Length > 0)
        {
            File.WriteAllBytes("graph.pdf", pdfBytes);
            Console.WriteLine($"PDF generated – {pdfBytes.Length} bytes written to 'graph.pdf'.");
        }
        else
        {
            Console.WriteLine("PDF generation skipped due to missing GDI+ (libgdiplus) on this platform.");
        }
    }
}