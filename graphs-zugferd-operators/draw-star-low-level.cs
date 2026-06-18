using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

namespace DrawStarLowLevel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with a single blank page
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Reopen the PDF and draw a star using low‑level operators
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = pdfDoc.Pages[1];

                // Get the operator collection for the page contents
                OperatorCollection operators = page.Contents;

                // Set line width and stroke color (red)
                operators.Add(new SetLineWidth(2.0f));
                operators.Add(new SetRGBColorStroke(1.0f, 0.0f, 0.0f));

                // Star parameters
                float centerX = 200.0f;
                float centerY = 500.0f;
                float outerRadius = 50.0f;
                float innerRadius = 20.0f;

                // Begin the path at the first outer point
                double angle0 = 0.0;
                float startX = centerX + outerRadius * (float)Math.Cos(angle0);
                float startY = centerY + outerRadius * (float)Math.Sin(angle0);
                operators.Add(new MoveTo(startX, startY));

                // Add the remaining points (alternating outer and inner)
                for (int i = 1; i < 10; i++)
                {
                    double angle = i * Math.PI / 5.0; // 36° increments
                    float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                    float x = centerX + radius * (float)Math.Cos(angle);
                    float y = centerY + radius * (float)Math.Sin(angle);
                    operators.Add(new LineTo(x, y));
                }

                // Close the path and fill & stroke it
                operators.Add(new ClosePathFillStroke());

                // Save the modified PDF
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
