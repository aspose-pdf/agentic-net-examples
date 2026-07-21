using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

public static class PdfFormExporter
{
    /// <summary>
    /// Asynchronously exports the form fields of a PDF document to an XML file.
    /// The operation is performed on a background thread to avoid blocking the UI thread.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file containing the form.</param>
    /// <param name="xmlPath">Path where the exported XML will be saved.</param>
    /// <returns>A task that completes when the export is finished.</returns>
    public static async Task ExportFormDataToXmlAsync(string pdfPath, string xmlPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (string.IsNullOrWhiteSpace(xmlPath))
            throw new ArgumentException("XML output path must be provided.", nameof(xmlPath));

        // Ensure the source PDF exists before proceeding.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // The Form class implements IDisposable, so we use a using block.
        using (Form form = new Form(pdfPath))
        // Open the output stream for the XML file.
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            // ExportXml is synchronous; run it on a thread‑pool thread.
            await Task.Run(() => form.ExportXml(xmlStream)).ConfigureAwait(false);
            // Ensure all data is flushed to disk.
            await xmlStream.FlushAsync().ConfigureAwait(false);
        }
    }
}

public class Program
{
    // Async entry point (C# 7.1+). Allows the demo to be run from the command line.
    public static async Task Main(string[] args)
    {
        // Expected arguments: <input.pdf> <output.xml>
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <app> <input.pdf> <output.xml>");
            return;
        }

        string pdfPath = args[0];
        string xmlPath = args[1];

        try
        {
            await PdfFormExporter.ExportFormDataToXmlAsync(pdfPath, xmlPath);
            Console.WriteLine("Form data exported successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
