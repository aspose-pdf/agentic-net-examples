using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ExportTotalPriceToXml
{
    static void Main()
    {
        // Sample data: list of (price, quantity) tuples
        var items = new List<(decimal price, int quantity)>
        {
            (19.99m, 2),
            (5.50m, 5),
            (12.30m, 1)
        };

        // Calculate total price
        decimal total = 0;
        foreach (var (price, quantity) in items)
            total += price * quantity;

        // Create a PDF document and add the total price as text (optional – the PDF is still generated)
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            Page page = pdfDoc.Pages.Add();

            // Create a text fragment with the total price
            TextFragment totalFragment = new TextFragment($"Total Price: {total:C}");
            totalFragment.TextState.FontSize = 14;
            totalFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            totalFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the fragment near the top of the page
            totalFragment.Position = new Position(100, 750);
            page.Paragraphs.Add(totalFragment);

            // Save the PDF (optional – not required for the XML export)
            string pdfPath = Path.Combine(Environment.CurrentDirectory, "TotalPrice.pdf");
            pdfDoc.Save(pdfPath);
        }

        // ---------- XML Export (no PDF‑to‑XML conversion required) ----------
        // Build a simple XML document containing the total price
        XDocument xmlDoc = new XDocument(
            new XElement("Invoice",
                new XElement("TotalPrice", total)
            )
        );

        // Define output XML path
        string xmlPath = Path.Combine(Environment.CurrentDirectory, "TotalPrice.xml");

        // Save the XML document
        xmlDoc.Save(xmlPath);

        Console.WriteLine("Total price exported to XML successfully.");
    }
}
