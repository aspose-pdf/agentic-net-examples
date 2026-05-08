using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "invoice.pdf";          // existing PDF with ZUGFeRD attachment
        const string outputPdfPath = "invoice_updated.pdf";  // PDF after attachment replacement
        const string newXmlContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<Invoice>
    <ID>2023-001</ID>
    <IssueDate>2023-04-01</IssueDate>
    <Seller>
        <Name>Acme Corp</Name>
        <TaxID>123456789</TaxID>
    </Seller>
    <Buyer>
        <Name>Globex Ltd</Name>
        <TaxID>987654321</TaxID>
    </Buyer>
    <TotalAmount>1500.00</TotalAmount>
</Invoice>";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare the new XML as a memory stream
                byte[] xmlBytes = System.Text.Encoding.UTF8.GetBytes(newXmlContent);
                using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
                {
                    bool attachmentReplaced = false;

                    // Search for an existing embedded XML file (ZUGFeRD)
                    foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                    {
                        // Guard against null Name
                        if (fileSpec?.Name != null &&
                            fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            // Replace the contents with the new XML
                            // Reset the stream position to the beginning
                            xmlStream.Position = 0;
                            fileSpec.Contents = new MemoryStream(xmlBytes);
                            attachmentReplaced = true;
                            break;
                        }
                    }

                    // If no XML attachment was found, add a new one
                    if (!attachmentReplaced)
                    {
                        // Create a new file specification for the ZUGFeRD XML
                        // The constructor parameters are (fileName, description)
                        FileSpecification newSpec = new FileSpecification("ZUGFeRD-invoice.xml",
                                                                          "ZUGFeRD XML invoice");
                        // Assign the XML content stream
                        newSpec.Contents = new MemoryStream(xmlBytes);
                        // Add the new specification to the document's embedded files collection
                        pdfDoc.EmbeddedFiles.Add(newSpec);
                    }
                }

                // Save the updated PDF (incremental update is performed automatically)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"ZUGFeRD attachment updated successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating attachment: {ex.Message}");
        }
    }
}