using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ExportTotalPriceToXml
{
    static void Main()
    {
        // Sample data: list of item prices
        decimal[] itemPrices = { 19.99m, 5.49m, 12.30m, 7.25m };
        decimal totalPrice = 0;
        foreach (decimal price in itemPrices)
            totalPrice += price;

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // ------------------------------------------------------------
            // The XML conversion requires a *tagged* PDF.  Mark the document
            // as tagged and create a minimal structure hierarchy.
            // ------------------------------------------------------------
            var tagged = pdfDoc.TaggedContent; // Use 'var' to avoid direct reference to TaggedContent type
            tagged.SetTitle("Total Price Document");
            tagged.SetLanguage("en-US");

            // Create a simple tagged structure (root → sect → div → art)
            var root = tagged.RootElement;
            var sect = tagged.CreateSectElement();
            root.AppendChild(sect);
            var div = tagged.CreateDivElement();
            sect.AppendChild(div);
            var art = tagged.CreateArtElement();
            div.AppendChild(art);

            // Add a page
            Page page = pdfDoc.Pages.Add();

            // Prepare the text to display the total price
            string priceText = $"Total Price: {totalPrice:C}";
            TextFragment fragment = new TextFragment(priceText);
            fragment.TextState.FontSize = 14;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document as XML using XmlSaveOptions
            string xmlOutputPath = Path.Combine(Environment.CurrentDirectory, "TotalPrice.xml");
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            pdfDoc.Save(xmlOutputPath, xmlOptions);
        }

        Console.WriteLine("Total price exported to XML successfully.");
    }
}
