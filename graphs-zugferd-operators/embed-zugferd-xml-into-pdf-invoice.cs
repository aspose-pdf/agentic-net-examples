using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and a temporary XML file
        const string outputPdfPath = "Invoice_With_ZUGFeRD.pdf";
        string tempXmlPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".xml");

        // -----------------------------------------------------------------
        // 1. Create a simple ZUGFeRD XML document from an example order
        // -----------------------------------------------------------------
        XDocument zugferdXml = new XDocument(
            new XElement("Invoice",
                new XElement("Header",
                    new XElement("InvoiceNumber", "INV-1001"),
                    new XElement("InvoiceDate", DateTime.Today.ToString("yyyy-MM-dd"))
                ),
                new XElement("Seller",
                    new XElement("Name", "Acme Corp"),
                    new XElement("TaxID", "DE123456789")
                ),
                new XElement("Buyer",
                    new XElement("Name", "Globex Ltd"),
                    new XElement("TaxID", "DE987654321")
                ),
                new XElement("Items",
                    new XElement("Item",
                        new XElement("Description", "Widget A"),
                        new XElement("Quantity", "10"),
                        new XElement("UnitPrice", "15.00"),
                        new XElement("TotalPrice", "150.00")
                    ),
                    new XElement("Item",
                        new XElement("Description", "Widget B"),
                        new XElement("Quantity", "5"),
                        new XElement("UnitPrice", "30.00"),
                        new XElement("TotalPrice", "150.00")
                    )
                ),
                new XElement("Totals",
                    new XElement("GrandTotal", "300.00")
                )
            )
        );

        // Save the XML to a temporary file – this file will be embedded into the PDF
        zugferdXml.Save(tempXmlPath);

        // -----------------------------------------------------------------
        // 2. Create a new PDF invoice and add some basic content
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            Page page = pdfDoc.Pages.Add();

            // Create a text fragment with invoice information
            TextFragment tf = new TextFragment("Invoice #INV-1001\nDate: " + DateTime.Today.ToString("yyyy-MM-dd") +
                                            "\n\nSeller: Acme Corp\nBuyer: Globex Ltd\n\nTotal: 300.00 EUR");
            tf.Position = new Position(50, 750); // Position near the top-left corner
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // -----------------------------------------------------------------
            // 3. Attach the ZUGFeRD XML file to the PDF using core API
            // -----------------------------------------------------------------
            // Create a FileSpecification for the XML file
            FileSpecification fileSpec = new FileSpecification(tempXmlPath);
            fileSpec.Description = "ZUGFeRD XML";

            // Add the file to the document's embedded files collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Optionally add a (invisible) file attachment annotation so the attachment appears in the PDF viewer's attachment pane
            // The rectangle is set to zero size because we only need the logical attachment, not a visible icon.
            Aspose.Pdf.Rectangle zeroRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, zeroRect, fileSpec);
            page.Annotations.Add(attachment);

            // -----------------------------------------------------------------
            // 4. Save the PDF invoice
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        // Clean up the temporary XML file
        if (File.Exists(tempXmlPath))
        {
            File.Delete(tempXmlPath);
        }

        Console.WriteLine($"PDF invoice with embedded ZUGFeRD XML saved to '{outputPdfPath}'.");
    }
}
