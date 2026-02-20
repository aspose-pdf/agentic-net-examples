using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF file path
        // 1 - output XML file path
        // 2 (optional) - output PDF file path (copy of the input)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <input.pdf> <output.xml> [output.pdf]");
            return;
        }

        string inputPdfPath = args[0];
        string outputXmlPath = args[1];
        string outputPdfPath = args.Length >= 3 ? args[2] : null;

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ---------- Export form fields to XML ----------
            using (Form formFacade = new Form())
            {
                // Bind the PDF document to the Form facade
                formFacade.BindPdf(inputPdfPath);

                // Export the form data to an XML stream
                using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                {
                    formFacade.ExportXml(xmlStream);
                }
            }

            Console.WriteLine($"Form data exported to XML successfully: {outputXmlPath}");

            // ---------- Optional: save (or copy) the PDF ----------
            if (!string.IsNullOrEmpty(outputPdfPath))
            {
                // Load the PDF with the high‑level Document API and save it
                using (Document pdfDocument = new Document(inputPdfPath))
                {
                    // Document.Save follows the provided document-save rule
                    pdfDocument.Save(outputPdfPath);
                }

                Console.WriteLine($"PDF saved to: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}