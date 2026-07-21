using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Asynchronous entry point
    static async Task Main(string[] args)
    {
        // Paths (adjust as needed)
        const string pdfInputPath   = "input.pdf";      // Existing PDF with form fields
        const string xmlFormDataPath = "formData.xml"; // XML containing form data (XFA)
        const string pdfOutputPath  = "output.pdf";

        // Optional cancellation token (e.g., from a UI or console cancellation)
        using CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        try
        {
            await ImportXmlFormDataAsync(pdfInputPath, xmlFormDataPath, pdfOutputPath, token);
            Console.WriteLine($"Form data imported and saved to '{pdfOutputPath}'.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Imports XML form data into a PDF and saves the result asynchronously.
    private static async Task ImportXmlFormDataAsync(
        string pdfPath,
        string xmlPath,
        string outputPath,
        CancellationToken cancellationToken = default)
    {
        // Validate input files
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");
        if (!File.Exists(xmlPath))
            throw new FileNotFoundException($"XML file not found: {xmlPath}");

        // Load the PDF document (synchronous constructor as per Aspose.Pdf API)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Asynchronously read the XML content to avoid blocking the main thread
            string xmlContent = await File.ReadAllTextAsync(xmlPath, cancellationToken).ConfigureAwait(false);

            // Parse the XML into an XmlDocument (required by AssignXfa)
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            // Assign the XFA data to the form (synchronous operation)
            pdfDocument.Form.AssignXfa(xmlDoc);

            // Asynchronously save the modified PDF
            await pdfDocument.SaveAsync(outputPath, cancellationToken).ConfigureAwait(false);
        }
    }
}