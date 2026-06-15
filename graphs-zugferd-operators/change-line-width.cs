using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

namespace ChangeLineWidthExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with a simple line of width 1 point
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                // Add operators: set line width 1, move to, line to, stroke
                SetLineWidth setWidth = new SetLineWidth(1.0);
                MoveTo move = new MoveTo(100.0, 700.0);
                LineTo line = new LineTo(400.0, 700.0);
                Stroke stroke = new Stroke();

                samplePage.Contents.Add(setWidth);
                samplePage.Contents.Add(move);
                samplePage.Contents.Add(line);
                samplePage.Contents.Add(stroke);

                sampleDoc.Save("input.pdf");
            }

            // Open the PDF and modify line width operators from 1 point to 3 points
            using (Document pdfDoc = new Document("input.pdf"))
            {
                int pageCount = pdfDoc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];
                    int operatorCount = page.Contents.Count;
                    for (int opIndex = 1; opIndex <= operatorCount; opIndex++)
                    {
                        Operator pdfOperator = page.Contents[opIndex];
                        if (pdfOperator is SetLineWidth)
                        {
                            SetLineWidth lineWidthOperator = (SetLineWidth)pdfOperator;
                            if (lineWidthOperator.Width == 1.0)
                            {
                                lineWidthOperator.Width = 3.0;
                            }
                        }
                    }
                }

                pdfDoc.Save("output.pdf");
            }
        }
    }
}
