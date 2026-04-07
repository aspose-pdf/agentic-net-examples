using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

namespace DrawStarLowLevel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document document = new Document())
            {
                // Add a page to the document
                Page page = document.Pages.Add();

                // Get the operator collection for the page's content stream
                OperatorCollection operators = page.Contents;

                // Set line width (stroke color will use default black)
                operators.Add(new SetLineWidth(1.0f));

                // Star parameters (only first two vertices will be used to stay within evaluation limit)
                int points = 5;
                float outerRadius = 100.0f;
                float innerRadius = 50.0f;
                float centerX = 200.0f;
                float centerY = 400.0f;

                // Calculate star vertices (alternating outer and inner points)
                List<float> xCoords = new List<float>();
                List<float> yCoords = new List<float>();
                for (int i = 0; i < points * 2; i++)
                {
                    double angleDeg = i * 36.0; // 360 / (points * 2)
                    double angleRad = Math.PI * angleDeg / 180.0;
                    float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                    float x = centerX + (float)(radius * Math.Cos(angleRad));
                    float y = centerY + (float)(radius * Math.Sin(angleRad));
                    xCoords.Add(x);
                    yCoords.Add(y);
                }

                // Begin path at the first vertex
                operators.Add(new MoveTo(xCoords[0], yCoords[0]));

                // Draw a line to the second vertex (limit to 4 operators total)
                operators.Add(new LineTo(xCoords[1], yCoords[1]));

                // Close the path and stroke it
                operators.Add(new ClosePathStroke());

                // Note: In Aspose.PDF evaluation mode a collection can contain at most 4 elements.
                // The above example demonstrates the operation within that limit.
                // With a full license you could add more operators to draw the complete star.

                // Save the PDF document
                document.Save("star.pdf");
            }
        }
    }
}
