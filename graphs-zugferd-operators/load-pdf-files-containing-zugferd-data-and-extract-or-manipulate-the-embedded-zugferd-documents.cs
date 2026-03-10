using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class ZugferdProcessor
{
    static void Main()
    {
        const string inputPdfPath = "invoice_with_zugferd.pdf";
        const string outputPdfPath = "invoice_modified.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF that contains ZUGFeRD data.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // The ZUGFeRD XML is stored as an embedded file (AF entry).
            // Iterate over all embedded files and locate the XML document.
            FileSpecification zugferdFile = null;
            for (int i = 1; i <= pdfDoc.EmbeddedFiles.Count; i++) // 1‑based indexing
            {
                FileSpecification fs = pdfDoc.EmbeddedFiles[i];
                if (fs.Name != null && fs.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    zugferdFile = fs;
                    break;
                }
            }

            if (zugferdFile == null)
            {
                Console.WriteLine("No embedded ZUGFeRD XML found in the PDF.");
                pdfDoc.Save(outputPdfPath); // just copy the original PDF
                return;
            }

            // Extract the original ZUGFeRD XML to a string.
            string originalXml;
            using (StreamReader reader = new StreamReader(zugferdFile.Contents, Encoding.UTF8))
            {
                originalXml = reader.ReadToEnd();
            }

            Console.WriteLine("Original ZUGFeRD XML extracted:");
            Console.WriteLine(originalXml);

            // Example manipulation: add a comment node at the beginning of the XML.
            string modifiedXml = $"<!-- Modified by ZugferdProcessor -->{Environment.NewLine}{originalXml}";

            // Replace the embedded file with the modified XML.
            // First remove the existing entry.
            pdfDoc.EmbeddedFiles.Delete(zugferdFile.Name);

            // Then add the new XML as an embedded file.
            var newSpec = new FileSpecification("ZUGFeRD-invoice.xml", "ZUGFeRD XML");
            newSpec.Contents = new MemoryStream(Encoding.UTF8.GetBytes(modifiedXml));
            pdfDoc.EmbeddedFiles.Add(newSpec);

            // Save the updated PDF.
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
        }
    }
}
