using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "xfa_data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Access the form object
                var form = pdfDoc.Form;

                // Check if XFA data is present using the correct properties
                if (form != null && form.HasXfa)
                {
                    // Retrieve the XFA object
                    XFA xfa = form.XFA;

                    // The XFA class does not expose a Save method.  The XML data can be
                    // obtained from the Datasets property (an XmlNode) and written to a file.
                    string xmlContent = xfa.Datasets?.OuterXml ?? string.Empty;
                    File.WriteAllText(outputXml, xmlContent);

                    Console.WriteLine($"XFA data extracted to '{outputXml}'.");
                }
                else
                {
                    Console.WriteLine("No XFA form data found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
