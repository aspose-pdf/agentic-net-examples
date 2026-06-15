using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Asynchronously imports XML (XFDF) form data into a PDF and saves the result.
    static async Task ImportXmlFormDataAsync(
        string sourcePdfPath,
        string xfdfPath,
        string outputPdfPath,
        CancellationToken cancellationToken = default)
    {
        // Validate input files.
        if (!File.Exists(sourcePdfPath))
            throw new FileNotFoundException($"Source PDF not found: {sourcePdfPath}");
        if (!File.Exists(xfdfPath))
            throw new FileNotFoundException($"XFDF file not found: {xfdfPath}");

        // Load the PDF document.
        using (Document pdfDocument = new Document(sourcePdfPath))
        {
            // Open the XFDF file as an asynchronous stream.
            await using (FileStream xfdfStream = new FileStream(
                xfdfPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true))
            {
                // Import field values from the XFDF stream.
                // XfdfReader.ReadFields is synchronous, so run it on a background thread.
                await Task.Run(() => XfdfReader.ReadFields(xfdfStream, pdfDocument), cancellationToken);
            }

            // Save the updated PDF asynchronously.
            await pdfDocument.SaveAsync(outputPdfPath, cancellationToken);
        }
    }

    // Example entry point.
    static async Task Main(string[] args)
    {
        // Paths can be adjusted as needed.
        const string inputPdf = "input.pdf";
        const string inputXfdf = "data.xfdf";
        const string outputPdf = "output.pdf";

        try
        {
            // Use a CancellationTokenSource to allow cancellation if required.
            using CancellationTokenSource cts = new CancellationTokenSource();

            await ImportXmlFormDataAsync(inputPdf, inputXfdf, outputPdf, cts.Token);
            Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}