using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "invoice.pdf";
        const string newXmlPath = "new_invoice.xml";
        const string outputPdfPath = "invoice_updated.pdf";
        const string attachmentName = "ZUGFeRD-invoice.xml"; // typical ZUGFeRD attachment name

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(newXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {newXmlPath}");
            return;
        }

        // Load the new ZUGFeRD XML content
        string newXmlContent = File.ReadAllText(newXmlPath, Encoding.UTF8);
        byte[] xmlBytes = Encoding.UTF8.GetBytes(newXmlContent);

        // Open the PDF, replace (or add) the ZUGFeRD attachment, and save
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Remove existing attachment with the same name, if present
            // EmbeddedFiles collection is accessed by name (string) – use Delete(string)
            try
            {
                // Attempt to delete; if the name does not exist an exception is thrown which we ignore
                pdfDoc.EmbeddedFiles.Delete(attachmentName);
            }
            catch { /* attachment not present – nothing to delete */ }

            // Add the new attachment
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                // Create a FileSpecification using the constructor that takes a file name and description
                var fileSpec = new FileSpecification(attachmentName, attachmentName);
                // Assign the XML content via the Contents stream property
                fileSpec.Contents = xmlStream;
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with updated ZUGFeRD attachment: {outputPdfPath}");
    }
}
