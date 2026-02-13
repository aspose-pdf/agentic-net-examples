using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string inputPdf = Path.Combine(dataDir, "input.pdf");
        string outputPdf = Path.Combine(dataDir, "output.pdf");
        string outputXml = Path.Combine(dataDir, "form-data.xml");

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // Ensure the document contains an AcroForm
            if (pdfDocument.Form != null && pdfDocument.Form.Count > 0)
            {
                // Iterate over form fields using the correct type (Field)
                foreach (Field field in pdfDocument.Form)
                {
                    // Process only text box fields (add more checks for other field types if needed)
                    if (field is TextBoxField textBox)
                    {
                        // Set a sample value; replace with real data as required
                        textBox.Value = "SampleValue";
                    }
                }
            }

            // Save the modified PDF
            pdfDocument.Save(outputPdf);

            // Export form data to XML
            var xmlOptions = new XmlSaveOptions();
            pdfDocument.Save(outputXml, xmlOptions);

            Console.WriteLine("PDF processing completed successfully.");
            Console.WriteLine($"Modified PDF saved to: {outputPdf}");
            Console.WriteLine($"Form data exported to XML: {outputXml}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
