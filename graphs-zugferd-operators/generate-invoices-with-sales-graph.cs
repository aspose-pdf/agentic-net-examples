using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Order
{
    public int Id { get; set; }
    public string? Customer { get; set; }
    public double Amount { get; set; }
    public List<double>? SalesData { get; set; }
}

class InvoiceGenerator
{
    static void Main()
    {
        const string templatePath = "invoice_template.pdf";
        const string outputDir = "Invoices";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Sample order data – replace with real data source as needed
        List<Order> orders = GetSampleOrders();

        foreach (Order order in orders)
        {
            // Load the invoice template for each order
            using (Document doc = new Document(templatePath))
            {
                // Assume the template has at least one page
                Page page = doc.Pages[1];

                // Add customer name
                TextFragment customerTf = new TextFragment($"Customer: {order.Customer}");
                customerTf.Position = new Position(100, 700);
                page.Paragraphs.Add(customerTf);

                // Add total amount
                TextFragment amountTf = new TextFragment($"Total: ${order.Amount:F2}");
                amountTf.Position = new Position(100, 680);
                page.Paragraphs.Add(amountTf);

                // Create a graph container (400x200) for the sales chart using the double constructor
                Graph graph = new Graph(400.0, 200.0);

                // Draw X axis
                float[] xAxis = { 0, 0, 400, 0 };
                Line xLine = new Line(xAxis);
                xLine.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1f };
                graph.Shapes.Add(xLine);

                // Draw Y axis
                float[] yAxis = { 0, 0, 0, 200 };
                Line yLine = new Line(yAxis);
                yLine.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1f };
                graph.Shapes.Add(yLine);

                // Plot sales data as a polyline (if data is available)
                if (order.SalesData != null && order.SalesData.Count > 1)
                {
                    // Determine scaling factor based on max value
                    double max = 0;
                    foreach (double v in order.SalesData) if (v > max) max = v;
                    if (max == 0) max = 1; // avoid division by zero

                    // Convert data points to graph coordinates
                    float[] points = new float[order.SalesData.Count * 2];
                    for (int i = 0; i < order.SalesData.Count; i++)
                    {
                        float x = (float)(i * 400.0 / (order.SalesData.Count - 1));
                        float y = (float)(order.SalesData[i] * 200.0 / max);
                        points[i * 2] = x;
                        points[i * 2 + 1] = y;
                    }

                    // Add line segments between consecutive points
                    for (int i = 0; i < points.Length - 2; i += 2)
                    {
                        float[] segment = { points[i], points[i + 1], points[i + 2], points[i + 3] };
                        Line segLine = new Line(segment);
                        segLine.GraphInfo = new GraphInfo { Color = Color.Blue, LineWidth = 2f };
                        graph.Shapes.Add(segLine);
                    }
                }

                // Place the graph on the page
                page.Paragraphs.Add(graph);

                // Save the personalized invoice
                string outPath = System.IO.Path.Combine(outputDir, $"Invoice_{order.Id}.pdf");
                doc.Save(outPath);
                Console.WriteLine($"Saved invoice: {outPath}");
            }
        }
    }

    // Mock method to provide sample orders; replace with actual data retrieval logic
    static List<Order> GetSampleOrders()
    {
        return new List<Order>
        {
            new Order
            {
                Id = 1,
                Customer = "Acme Corp",
                Amount = 1234.56,
                SalesData = new List<double> { 100, 150, 200, 250, 300 }
            },
            new Order
            {
                Id = 2,
                Customer = "Beta Ltd",
                Amount = 789.00,
                SalesData = new List<double> { 80, 120, 160, 200, 240 }
            }
        };
    }
}
