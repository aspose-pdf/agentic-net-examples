using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string xmlAttachment  = "metadata.xml";   // XML file to attach
        const string outputPdfPath  = "output_pdfa1b.pdf"; // result PDF/A‑1b file

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlAttachment))
        {
            Console.Error.WriteLine($"XML attachment not found: {xmlAttachment}");
            return;
        }

        try
        {
            // Load the original PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Convert to PDF/A‑1b (compliance is applied in‑place)
                doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Create a file specification for the XML attachment
                // Constructor: FileSpecification(string filePath, string description)
                FileSpecification fileSpec = new FileSpecification(xmlAttachment, "External XML metadata");

                // Add the attachment to the document
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the PDF/A‑1b document with the embedded XML file
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A‑1b file created with XML attachment: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}