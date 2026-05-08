using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the input XML file and the output PDF file.
        const string xmlPath = "discounts.xml";
        const string pdfPath = "price_report.pdf";

        // Validate that the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load discount rates from the XML file.
        // Expected XML format:
        // <Discounts>
        //   <Discount>0.10</Discount>   <!-- 10% discount -->
        //   <Discount>0.05</Discount>   <!-- 5% discount -->
        // </Discounts>
        XDocument xDoc = XDocument.Load(xmlPath);
        double[] discountRates = xDoc.Root
                                      .Elements("Discount")
                                      .Select(e => (double) e)
                                      .ToArray();

        // Example base price. In a real scenario this could come from user input or another source.
        double basePrice = 100.00;

        // Apply discounts sequentially: price = price * (1 - discountRate)
        double totalPrice = basePrice;
        foreach (double rate in discountRates)
        {
            totalPrice *= (1.0 - rate);
        }

        // Create a new PDF document and add the calculation result.
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document.
            Page page = pdfDoc.Pages.Add();

            // Build the result text.
            string resultText = $"Base price: {basePrice:C}\n" +
                                $"Applied discounts: {string.Join(", ", discountRates.Select(r => r.ToString("P")))}\n" +
                                $"Total price after discounts: {totalPrice:C}";

            // Create a text fragment with the result.
            TextFragment fragment = new TextFragment(resultText);
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the page.
            page.Paragraphs.Add(fragment);

            // Save the PDF document.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Price report saved to '{pdfPath}'.");
    }
}