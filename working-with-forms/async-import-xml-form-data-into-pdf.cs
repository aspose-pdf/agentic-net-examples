using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Entry point – async to allow awaiting the import operation
    static async Task Main(string[] args)
    {
        const string inputPdf   = "input.pdf";    // source PDF with a form
        const string inputXml   = "formData.xml"; // XML containing form data (XFA)
        const string outputPdf  = "output.pdf";   // destination PDF

        try
        {
            await ImportXmlFormDataAsync(inputPdf, inputXml, outputPdf);
            Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Asynchronously imports XML form data into a PDF and saves it
    private static async Task ImportXmlFormDataAsync(
        string pdfPath,
        string xmlPath,
        string outputPath,
        CancellationToken cancellationToken = default)
    {
        // Ensure the source PDF exists – if not, create a minimal PDF with a form field
        if (!File.Exists(pdfPath))
        {
            CreatePlaceholderPdf(pdfPath);
        }

        // Ensure the XML file exists – give a clear error if it does not
        if (!File.Exists(xmlPath))
        {
            throw new FileNotFoundException($"XML data file '{xmlPath}' was not found.");
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Read the XML file without blocking the thread
            string xmlContent = await File.ReadAllTextAsync(xmlPath, cancellationToken)
                                         .ConfigureAwait(false);

            // Parse the XML into an XmlDocument (required by AssignXfa)
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            // Import the XML form data (XFA) into the PDF form
            pdfDocument.Form.AssignXfa(xmlDoc);

            // Save the updated PDF asynchronously – the Save method is CPU‑bound, so wrap it in Task.Run
            await Task.Run(() => pdfDocument.Save(outputPath), cancellationToken)
                      .ConfigureAwait(false);
        }
    }

    // Helper: creates a very simple PDF containing a single text box field.
    // This allows the example to run even when the expected input file is missing.
    private static void CreatePlaceholderPdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text box field named "SampleField"
            TextBoxField txtField = new TextBoxField(page, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "SampleField",
                Value = "Placeholder"
            };
            doc.Form.Add(txtField);

            // Save the placeholder PDF
            doc.Save(path);
        }
    }
}
