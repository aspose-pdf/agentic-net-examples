using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlFile   = "data.xml";
        const string outputPdf = "output_pdfa.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Convert the document to PDF/A‑1b compliance
                doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Embed the external XML file as an embedded file (attachment)
                using (FileStream xmlStream = File.OpenRead(xmlFile))
                {
                    // Create a file specification for the XML file
                    var fileSpec = new FileSpecification("data.xml", "XML Data");
                    fileSpec.Contents = xmlStream; // assign the file data
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Save the PDF/A‑1b document with the embedded XML file
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑1b with XML attachment saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
