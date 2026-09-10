using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the PDF, the ZUGFeRD XML data, the output PDF and a conversion log.
        const string outputPdfPath = "invoice_zugferd.pdf";
        const string xmlDataPath    = "invoice.xml";
        const string conversionLog  = "conversion.log";

        // Verify that the XML file exists.
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Create a new PDF document and add simple invoice content.
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Invoice title.
            TextFragment title = new TextFragment("Invoice");
            title.TextState.FontSize = 20;
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            page.Paragraphs.Add(title);

            // Placeholder invoice details.
            TextFragment details = new TextFragment(
                "Customer: Acme Corp\n" +
                "Amount: $1,234.56\n" +
                "Date: 2023-12-31");
            details.TextState.FontSize = 12;
            details.TextState.Font = FontRepository.FindFont("Helvetica");
            details.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            page.Paragraphs.Add(details);

            // Embed the ZUGFeRD XML data into the PDF.
            doc.BindXml(xmlDataPath);

            // Convert the PDF to ZUGFeRD format, logging any conversion errors.
            doc.Convert(conversionLog, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

            // Save the resulting ZUGFeRD-compliant PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"ZUGFeRD invoice saved to '{outputPdfPath}'.");
    }
}