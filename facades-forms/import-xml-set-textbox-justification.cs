using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

// Alias the Facade Form to avoid conflict with Aspose.Pdf.Forms.Form
using PdfFormFacade = Aspose.Pdf.Facades.Form;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string xmlData = "data.xml";
        const string outputPdf = "filled.pdf";
        const string targetField = "TextBox1"; // change to your actual field name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xmlData))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlData}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        // Bind the document to the Facade Form (Aspose.Pdf.Facades.Form)
        using (PdfFormFacade form = new PdfFormFacade(doc))
        {
            // Import XML data (ImportXml expects a Stream)
            using (FileStream xmlStream = new FileStream(xmlData, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Locate the textbox field in the document's form collection
            var textbox = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == targetField) as TextBoxField;
            if (textbox != null)
            {
                // Set horizontal justification – use TextHorizontalAlignment with Aspose.Pdf.HorizontalAlignment
                textbox.TextHorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
            }
            else
            {
                Console.Error.WriteLine($"Field '{targetField}' not found or is not a TextBoxField.");
            }

            // Save the modified PDF via the facade
            form.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
