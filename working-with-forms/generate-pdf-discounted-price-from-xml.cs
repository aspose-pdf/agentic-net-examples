using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "discounts.xml";
        const string outputPdf = "price.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load discount rates from the XML file
        double totalDiscount = 0.0;
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList discountNodes = xmlDoc.SelectNodes("//Discount");
            foreach (XmlNode node in discountNodes)
            {
                if (double.TryParse(node.InnerText, out double rate))
                {
                    totalDiscount += rate;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XML: {ex.Message}");
            return;
        }

        // Example base price and calculation
        double basePrice = 100.0;
        double discountedPrice = basePrice * (1 - totalDiscount);
        string resultText = $"Base price: {basePrice:C}\n" +
                            $"Total discount: {totalDiscount:P}\n" +
                            $"Price after discount: {discountedPrice:C}";

        // Create a PDF document and add the result text
        using (Document pdfDoc = new Document())
        {
            // Add a new page
            Page page = pdfDoc.Pages.Add();

            // Create a TextFragment with the desired appearance
            TextFragment tf = new TextFragment(resultText);
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Color.Black;

            page.Paragraphs.Add(tf);

            // Save the PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
