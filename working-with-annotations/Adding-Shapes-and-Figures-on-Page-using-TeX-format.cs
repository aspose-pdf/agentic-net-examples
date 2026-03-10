using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string texInputPath = "sample.tex";      // TeX source file
        const string outputPdfPath = "output.pdf";

        // Ensure the TeX source exists
        if (!File.Exists(texInputPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texInputPath}");
            return;
        }

        // Load the TeX file into a PDF document using TeXLoadOptions
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document pdfDoc = new Document(texInputPath, texLoadOptions))
        {
            // Get the first page (pages are 1‑based)
            Page page = pdfDoc.Pages[1];

            // -------------------------------------------------
            // 1. Add a TeX fragment (rendered LaTeX) to the page
            // -------------------------------------------------
            // Example LaTeX formula
            string latex = @"\int_{0}^{\infty} e^{-x^2} dx = \frac{\sqrt{\pi}}{2}";
            // Create the fragment; second parameter 'true' removes leading indents
            TeXFragment texFragment = new TeXFragment(latex, true);
            // Optional: set alignment or hyperlink if needed
            texFragment.HorizontalAlignment = HorizontalAlignment.Center;
            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(texFragment);

            // -------------------------------------------------
            // 2. Add vector shapes (rectangle and ellipse) using Graph
            // -------------------------------------------------
            // Use double parameters as the constructor with float is obsolete
            Graph graph = new Graph(500.0, 300.0);

            // Rectangle shape – note the use of Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 200, 150, 100); // left, bottom, width, height
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Ellipse shape
            Ellipse ellipseShape = new Ellipse(250, 200, 150, 100);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipseShape);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // -------------------------------------------------
            // 3. Save the resulting PDF
            // -------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created successfully: {outputPdfPath}");
    }
}
