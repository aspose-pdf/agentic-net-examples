using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class FormDataExporter
{
    // Asynchronously exports form fields from a PDF to an XML file.
    // The operation is performed on a background thread to keep the UI responsive.
    public static async Task ExportFormDataToXmlAsync(
        string pdfFilePath,
        string xmlOutputPath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(pdfFilePath))
            throw new ArgumentException("PDF file path must be provided.", nameof(pdfFilePath));

        if (string.IsNullOrWhiteSpace(xmlOutputPath))
            throw new ArgumentException("XML output path must be provided.", nameof(xmlOutputPath));

        if (!File.Exists(pdfFilePath))
            throw new FileNotFoundException("Input PDF file not found.", pdfFilePath);

        // Run the blocking ExportXml call on a thread‑pool thread.
        await Task.Run(() =>
        {
            // Respect cancellation before starting the operation.
            cancellationToken.ThrowIfCancellationRequested();

            // Form implements IDisposable via SaveableFacade, so use using.
            using (Form form = new Form(pdfFilePath))
            {
                // Create the output stream for the XML file.
                using (FileStream outputStream = new FileStream(
                    xmlOutputPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: false)) // ExportXml is synchronous; use false here.
                {
                    form.ExportXml(outputStream);
                }
            }
        }, cancellationToken).ConfigureAwait(false);
    }
}

// Example usage in an async context (e.g., UI event handler).
// Note: The Main method is declared async for demonstration purposes.
class Program
{
    static async Task Main(string[] args)
    {
        const string inputPdf = "PdfForm.pdf";
        const string outputXml = "export.xml";

        try
        {
            // Optionally create a CancellationTokenSource to allow cancellation.
            using CancellationTokenSource cts = new CancellationTokenSource();

            await FormDataExporter.ExportFormDataToXmlAsync(inputPdf, outputXml, cts.Token);
            Console.WriteLine($"Form data exported successfully to '{outputXml}'.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Export operation was canceled.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}