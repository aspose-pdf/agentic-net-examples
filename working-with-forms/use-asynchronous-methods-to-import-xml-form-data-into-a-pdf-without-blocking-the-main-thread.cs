using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Asynchronously imports XML form data (XFDF) into a PDF and saves the result.
    static async Task ImportXmlFormDataAsync(string pdfInputPath, string xfdfPath, string pdfOutputPath, CancellationToken cancellationToken = default)
    {
        // Validate input files.
        if (!File.Exists(pdfInputPath))
            throw new FileNotFoundException($"PDF file not found: {pdfInputPath}");
        if (!File.Exists(xfdfPath))
            throw new FileNotFoundException($"XFDF file not found: {xfdfPath}");

        // Load the PDF document.
        using (Document pdfDocument = new Document(pdfInputPath))
        {
            // Open the XFDF (XML) stream.
            await using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Import the field values from XFDF. This operation is CPU‑bound,
                // so we run it on a background thread to avoid blocking the caller.
                await Task.Run(() => XfdfReader.ReadFields(xfdfStream, pdfDocument), cancellationToken);
            }

            // Save the updated PDF asynchronously.
            await pdfDocument.SaveAsync(pdfOutputPath, cancellationToken);
        }
    }

    // Entry point.
    static async Task Main(string[] args)
    {
        // Example usage:
        // args[0] = input PDF path
        // args[1] = XFDF (XML) form data path
        // args[2] = output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <input.pdf> <data.xfdf> <output.pdf>");
            return;
        }

        string inputPdf   = args[0];
        string xfdfFile   = args[1];
        string outputPdf  = args[2];

        try
        {
            // Pass a CancellationToken if you need to support cancellation.
            await ImportXmlFormDataAsync(inputPdf, xfdfFile, outputPdf);
            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}