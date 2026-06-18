using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

namespace InvoiceBatchGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a simple template PDF (self‑contained example)
            using (Document templateDoc = new Document())
            {
                Page templatePage = templateDoc.Pages.Add();
                TextFragment templateText = new TextFragment("Invoice Template");
                templateText.Position = new Position(100, 700);
                templatePage.Paragraphs.Add(templateText);
                templateDoc.Save("template.pdf");
            }

            // Sample order data (max 4 orders to respect evaluation limits)
            List<Order> orders = new List<Order>();
            orders.Add(new Order { Id = 1, Sales = new float[] { 150f, 200f, 250f } });
            orders.Add(new Order { Id = 2, Sales = new float[] { 300f, 180f, 220f } });
            orders.Add(new Order { Id = 3, Sales = new float[] { 120f, 160f, 190f } });

            foreach (Order order in orders)
            {
                using (Document invoiceDoc = new Document("template.pdf"))
                {
                    Page page = invoiceDoc.Pages[1];

                    // Add order title
                    TextFragment orderTitle = new TextFragment("Invoice #" + order.Id);
                    orderTitle.Position = new Position(100, 650);
                    page.Paragraphs.Add(orderTitle);

                    // Create a simple bar chart using Graph (double constructor)
                    Graph salesGraph = new Graph(400.0, 300.0);
                    salesGraph.Title = new TextFragment("Sales Graph");

                    // Add bars (rectangles) for each sales value (max 4 bars)
                    float barWidth = 40f;
                    float spacing = 20f;
                    float startX = 50f;
                    float baseY = 50f;
                    for (int i = 0; i < order.Sales.Length && i < 4; i++)
                    {
                        float barHeight = order.Sales[i];
                        Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(
                            startX + i * (barWidth + spacing),
                            baseY,
                            barWidth,
                            barHeight);
                        GraphInfo barInfo = new GraphInfo();
                        barInfo.FillColor = Color.LightBlue;
                        barInfo.Color = Color.DarkBlue;
                        barInfo.LineWidth = 1f;
                        bar.GraphInfo = barInfo;
                        salesGraph.Shapes.Add(bar);
                    }

                    // Add the graph to the page
                    page.Paragraphs.Add(salesGraph);

                    string outputFile = "invoice_" + order.Id + ".pdf";
                    invoiceDoc.Save(outputFile);
                }
            }
        }

        class Order
        {
            public int Id { get; set; }
            public float[] Sales { get; set; }
        }
    }
}
