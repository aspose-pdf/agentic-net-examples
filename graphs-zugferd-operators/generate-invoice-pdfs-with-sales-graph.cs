using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

namespace InvoiceGenerator
{
    // Simple data model for an order
    public class Order
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        // Sales data: list of (month, amount) pairs
        public List<(int Month, double Amount)> SalesData { get; set; } = new();
    }

    public static class Program
    {
        // Entry point
        public static void Main()
        {
            // Path to the PDF template that contains placeholders for invoice details
            const string templatePath = "InvoiceTemplate.pdf";

            // Ensure the template exists – create a minimal one if it does not.
            EnsureTemplateExists(templatePath);

            // Output directory for generated invoices
            const string outputDir = "GeneratedInvoices";

            // Ensure output directory exists
            Directory.CreateDirectory(outputDir);

            // Sample orders – in real scenario this would come from a database or service
            List<Order> orders = GetSampleOrders();

            // Process each order
            foreach (Order order in orders)
            {
                // Load a fresh copy of the template for each invoice
                using (Document doc = new Document(templatePath))
                {
                    // Assume the first page contains the invoice layout
                    Page page = doc.Pages[1]; // 1‑based indexing (page-indexing-one-based rule)

                    // Fill in textual placeholders (simplified example)
                    AddInvoiceDetails(page, order);

                    // Add a sales graph based on the order's sales data
                    AddSalesGraph(page, order.SalesData);

                    // Build output file name
                    string outputPath = System.IO.Path.Combine(outputDir,
                        $"Invoice_{order.InvoiceNumber}.pdf");

                    // Save the populated invoice (PDF format, no SaveOptions needed)
                    doc.Save(outputPath); // document-disposal-with-using rule ensures proper disposal
                }

                Console.WriteLine($"Generated invoice: {order.InvoiceNumber}");
            }
        }

        // Creates a very simple template PDF if it does not already exist.
        private static void EnsureTemplateExists(string path)
        {
            if (File.Exists(path))
                return;

            using (Document template = new Document())
            {
                // Add a single blank page – you could add static text/logo here if desired.
                Page page = template.Pages.Add();
                TextFragment title = new TextFragment("Invoice Template – Auto‑Generated")
                {
                    Position = new Position(200, 750)
                };
                title.TextState.Font = FontRepository.FindFont("Helvetica");
                title.TextState.FontSize = 16;
                title.TextState.ForegroundColor = Color.Gray;
                page.Paragraphs.Add(title);

                template.Save(path);
            }
        }

        // Populates basic invoice information on the page
        private static void AddInvoiceDetails(Page page, Order order)
        {
            // Invoice number
            TextFragment invoiceNumber = new TextFragment($"Invoice #: {order.InvoiceNumber}")
            {
                Position = new Position(50, 750)
            };
            invoiceNumber.TextState.Font = FontRepository.FindFont("Helvetica");
            invoiceNumber.TextState.FontSize = 12;
            invoiceNumber.TextState.ForegroundColor = Color.Black;
            page.Paragraphs.Add(invoiceNumber);

            // Customer name
            TextFragment customer = new TextFragment($"Bill To: {order.CustomerName}")
            {
                Position = new Position(50, 730)
            };
            customer.TextState.Font = FontRepository.FindFont("Helvetica");
            customer.TextState.FontSize = 12;
            customer.TextState.ForegroundColor = Color.Black;
            page.Paragraphs.Add(customer);

            // Invoice date
            TextFragment date = new TextFragment($"Date: {order.InvoiceDate:yyyy-MM-dd}")
            {
                Position = new Position(50, 710)
            };
            date.TextState.Font = FontRepository.FindFont("Helvetica");
            date.TextState.FontSize = 12;
            date.TextState.ForegroundColor = Color.Black;
            page.Paragraphs.Add(date);
        }

        // Draws a simple line chart representing monthly sales
        private static void AddSalesGraph(Page page, List<(int Month, double Amount)> salesData)
        {
            if (salesData == null || salesData.Count == 0)
                return; // nothing to draw

            // Define graph dimensions (points)
            const double graphWidth = 400;
            const double graphHeight = 200;

            // Create a Graph container
            Graph graph = new Graph(graphWidth, graphHeight);

            // Draw X and Y axes
            DrawAxis(graph, graphWidth, graphHeight);

            // Determine scaling factors
            double maxAmount = 0;
            foreach (var (_, amount) in salesData)
                if (amount > maxAmount) maxAmount = amount;
            if (maxAmount == 0) maxAmount = 1; // avoid division by zero

            double xStep = graphWidth / (salesData.Count - 1);
            double yScale = graphHeight / maxAmount;

            // Build points for the line series
            List<float> points = new List<float>();
            for (int i = 0; i < salesData.Count; i++)
            {
                double x = i * xStep;
                double y = salesData[i].Amount * yScale;
                points.Add((float)x);
                points.Add((float)(graphHeight - y)); // invert Y because PDF origin is bottom‑left
            }

            // Create the line shape
            Line salesLine = new Line(points.ToArray())
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Blue,
                    LineWidth = 2
                }
            };
            graph.Shapes.Add(salesLine);

            // Position the graph on the page (using a margin from the left/bottom)
            // The Graph itself does not have a Position property, so we embed it in a Paragraph with a margin.
            // Adding directly to the page will place it at the origin of the page.
            page.Paragraphs.Add(graph);
        }

        // Helper to draw X and Y axes inside the graph container
        private static void DrawAxis(Graph graph, double width, double height)
        {
            // X‑axis (bottom line)
            Line xAxis = new Line(new float[] { 0, (float)height, (float)width, (float)height })
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Black,
                    LineWidth = 1
                }
            };
            graph.Shapes.Add(xAxis);

            // Y‑axis (left line)
            Line yAxis = new Line(new float[] { 0, 0, 0, (float)height })
            {
                GraphInfo = new GraphInfo
                {
                    Color = Color.Black,
                    LineWidth = 1
                }
            };
            graph.Shapes.Add(yAxis);
        }

        // Generates a few sample orders for demonstration purposes
        private static List<Order> GetSampleOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    InvoiceNumber = "INV-1001",
                    CustomerName = "Acme Corp.",
                    InvoiceDate = DateTime.Today,
                    SalesData = new List<(int, double)>
                    {
                        (1, 1200), (2, 1500), (3, 1100), (4, 1700), (5, 1300)
                    }
                },
                new Order
                {
                    InvoiceNumber = "INV-1002",
                    CustomerName = "Globex Ltd.",
                    InvoiceDate = DateTime.Today,
                    SalesData = new List<(int, double)>
                    {
                        (1, 800), (2, 950), (3, 1020), (4, 870), (5, 1150)
                    }
                }
                // Add more orders as needed
            };
        }
    }
}
