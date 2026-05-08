using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";          // PDF with a form
        const string xmlDataPath  = "formData.xml";       // XML file containing XFA data
        const string outputPdfPath = "output.pdf";

        // Verify PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Check if the PDF actually contains an XFA form
            if (!pdfDoc.Form.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form. No XML import performed.");
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Saved PDF without changes to '{outputPdfPath}'.");
                return;
            }

            // Verify XML file exists before attempting import
            if (!File.Exists(xmlDataPath))
            {
                Console.Error.WriteLine($"Warning: XML import file not found: {xmlDataPath}");
                // Optionally, you could decide to abort or continue without import.
                // Here we continue and save the original PDF.
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Saved PDF without XML import to '{outputPdfPath}'.");
                return;
            }

            try
            {
                // Load XML into an XmlDocument
                XmlDocument xfaXml = new XmlDocument();
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    xfaXml.Load(xmlStream);
                }

                // Assign the XFA data to the form (ImportDataAction is for FDF; XFA uses AssignXfa)
                pdfDoc.Form.AssignXfa(xfaXml);
                Console.WriteLine("XML data successfully imported into the XFA form.");
            }
            catch (XmlException xmlEx)
            {
                // Handle malformed XML
                Console.Error.WriteLine($"Error: Failed to parse XML file '{xmlDataPath}'. Details: {xmlEx.Message}");
                // Decide whether to continue or abort; here we abort the import but still save the PDF.
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors during import
                Console.Error.WriteLine($"Error: Unexpected exception during XML import. Details: {ex.Message}");
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        }
    }
}