using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF with form fields
        const string xmlDataPath   = "formData.xml";       // XML file containing form data
        const string outputPdfPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Check for the XML import file before attempting to bind it
            if (File.Exists(xmlDataPath))
            {
                try
                {
                    // Bind the XML data to the PDF. No explicit XmlLoadOptions are needed for BindXml.
                    pdfDoc.BindXml(xmlDataPath);
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors during the bind operation
                    Console.Error.WriteLine($"Warning: Failed to bind XML data – {ex.Message}");
                }
            }
            else
            {
                // XML file is missing – log the situation and continue without importing data
                Console.Error.WriteLine($"Warning: XML import file not found – '{xmlDataPath}'. Form data will not be imported.");
            }

            // Save the (potentially modified) PDF
            try
            {
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Failed to save PDF – {ex.Message}");
            }
        }
    }
}