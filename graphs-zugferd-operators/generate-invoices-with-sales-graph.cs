using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Order
{
    public string OrderId { get; set; }
    public string CustomerName { get; set; }
    public string ChartImagePath { get; set; }

    public Order(string orderId, string customerName, string chartImagePath)
    {
        OrderId = orderId;
        CustomerName = customerName;
        ChartImagePath = chartImagePath;
    }
}

class Program
{
    static void Main()
    {
        // Path to the PDF template that contains static layout (logo, headings, etc.)
        const string templatePath = "InvoiceTemplate.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Sample orders – in a real scenario this could come from a database or service
        var orders = new List<Order>
        {
            new Order("1001", "Acme Corp", "chart1.png"),
            new Order("1002", "Beta Ltd", "chart2.png")
        };

        foreach (var order in orders)
        {
            // Load a fresh copy of the template for each invoice
            using (Document doc = new Document(templatePath))
            {
                // Assume the first page is where we place dynamic content
                Page page = doc.Pages[1];

                // ----- Add Order ID -----
                TextFragment orderIdFragment = new TextFragment($"Order ID: {order.OrderId}");
                // Position is measured from the bottom‑left corner of the page
                orderIdFragment.Position = new Aspose.Pdf.Text.Position(50, 750);
                orderIdFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                orderIdFragment.TextState.FontSize = 12;
                page.Paragraphs.Add(orderIdFragment);

                // ----- Add Customer Name -----
                TextFragment customerFragment = new TextFragment($"Customer: {order.CustomerName}");
                customerFragment.Position = new Aspose.Pdf.Text.Position(50, 730);
                customerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                customerFragment.TextState.FontSize = 12;
                page.Paragraphs.Add(customerFragment);

                // ----- Insert Sales Graph (chart image) -----
                if (File.Exists(order.ChartImagePath))
                {
                    ImageStamp imgStamp = new ImageStamp(order.ChartImagePath);
                    // Position the image using indents from the left and bottom edges
                    imgStamp.XIndent = 50;   // distance from left margin
                    imgStamp.YIndent = 400;  // distance from bottom margin
                    imgStamp.Width = 300;    // desired width in points
                    imgStamp.Height = 200;   // desired height in points
                    imgStamp.HorizontalAlignment = HorizontalAlignment.Left;
                    imgStamp.VerticalAlignment = VerticalAlignment.Bottom;

                    // Add the stamp (image) to the page
                    page.AddStamp(imgStamp);
                }
                else
                {
                    Console.Error.WriteLine($"Chart image not found: {order.ChartImagePath}");
                }

                // Save the generated invoice
                string outputPath = $"Invoice_{order.OrderId}.pdf";
                doc.Save(outputPath);
                Console.WriteLine($"Invoice saved: {outputPath}");
            }
        }
    }
}