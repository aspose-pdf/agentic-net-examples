using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ExportTotalPriceToXml
{
    static void Main()
    {
        // Example data: calculate total price (could be from any source)
        double totalPrice = 1234.56; // replace with actual calculation logic

        // Path for the temporary PDF that will hold the total price
        string pdfPath = Path.Combine(Path.GetTempPath(), "TotalPrice.pdf");
        // Path for the resulting XML file
        string xmlPath = Path.Combine(Path.GetTempPath(), "TotalPrice.xml");

        // Create a new PDF document, add a page and write the total price
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            Page page = pdfDoc.Pages.Add();

            // Create a text fragment with the total price
            TextFragment priceFragment = new TextFragment($"Total Price: {totalPrice:C}");
            priceFragment.TextState.FontSize = 14;
            priceFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            priceFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the fragment near the top of the page
            priceFragment.Position = new Position(100, 750);
            page.Paragraphs.Add(priceFragment);

            // Save the PDF (optional, can be omitted if only XML output is needed)
            pdfDoc.Save(pdfPath);

            // Export the PDF content to XML using MobiXmlSaveOptions (works for untagged PDFs)
            MobiXmlSaveOptions xmlOptions = new MobiXmlSaveOptions();
            pdfDoc.Save(xmlPath, xmlOptions);
        }

        Console.WriteLine($"Total price exported to XML at: {xmlPath}");
    }
}
