using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "invoice.pdf";
        const string xmlPath = "invoice.xml";
        const string validationLog = "validation.log";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Add simple invoice text
            TextFragment header = new TextFragment("Invoice #12345");
            header.TextState.FontSize = 18;
            header.TextState.Font = FontRepository.FindFont("Helvetica");
            header.Position = new Position(50, 750);
            page.Paragraphs.Add(header);

            TextFragment body = new TextFragment("Date: 2024-03-25\nCustomer: Acme Corp\nTotal: $1,250.00");
            body.TextState.FontSize = 12;
            body.Position = new Position(50, 720);
            page.Paragraphs.Add(body);

            // Bind ZUGFeRD XML data to the PDF
            doc.BindXml(xmlPath);

            // Save the PDF (ZUGFeRD compliance will be checked via validation)
            doc.Save(outputPdf);

            // Validate the generated PDF against the ZUGFeRD format
            bool isValid = doc.Validate(validationLog, PdfFormat.ZUGFeRD);
            Console.WriteLine(isValid ? "ZUGFeRD validation succeeded." : "ZUGFeRD validation failed. See log for details.");
        }
    }
}