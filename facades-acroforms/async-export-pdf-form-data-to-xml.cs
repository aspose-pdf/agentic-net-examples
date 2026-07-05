using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Asynchronously exports form fields of a PDF to an XML file.
    // The ExportXml method is synchronous, so it is wrapped in Task.Run
    // to avoid blocking the UI thread.
    static async Task ExportFormDataToXmlAsync(string pdfPath, string xmlPath)
    {
        // Validate input arguments.
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
        if (string.IsNullOrWhiteSpace(xmlPath))
            throw new ArgumentException("XML output path must be provided.", nameof(xmlPath));
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Run the blocking ExportXml operation on a background thread.
        await Task.Run(() =>
        {
            // Form implements IDisposable via SaveableFacade, so use using.
            using (Form form = new Form(pdfPath))
            // Create the output stream for the XML file.
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form data to the XML stream.
                form.ExportXml(xmlStream);
                // The using statements ensure both Form and FileStream are disposed.
            }
        });
    }

    // Example entry point demonstrating usage.
    // In a UI application this method could be called from an event handler.
    static async Task Main(string[] args)
    {
        // Example file paths (adjust as needed).
        const string inputPdf = "PdfForm.pdf";
        const string outputXml = "exported_form_data.xml";

        try
        {
            await ExportFormDataToXmlAsync(inputPdf, outputXml);
            Console.WriteLine($"Form data exported successfully to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}