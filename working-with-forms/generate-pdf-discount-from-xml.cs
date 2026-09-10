using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "discounts.xml";
        const string outputPdf = "TotalPrice.pdf";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load discount rates from the XML file
        // Expected XML format:
        // <Discounts>
        //   <Discount rate="0.10" />
        //   <Discount rate="0.05" />
        // </Discounts>
        XDocument xdoc = XDocument.Load(xmlPath);
        double totalDiscount = 0.0;
        foreach (var discountElem in xdoc.Descendants("Discount"))
        {
            if (double.TryParse(discountElem.Attribute("rate")?.Value, out double rate))
            {
                totalDiscount += rate;
            }
        }

        // Example base price (could be retrieved from elsewhere)
        double basePrice = 100.0;

        // Apply the accumulated discount (ensure it does not exceed 100%)
        double finalPrice = basePrice * Math.Max(0.0, 1.0 - totalDiscount);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Prepare the text to display
            string resultText = $"Base price: {basePrice:C}\n" +
                                $"Total discount: {totalDiscount:P}\n" +
                                $"Final price: {finalPrice:C}";

            // Create a TextFragment with the result
            TextFragment tf = new TextFragment(resultText);

            // Modify the existing TextState (the property is read‑only)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Save the PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
    }
}
