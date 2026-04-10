using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

// Minimal stub to emulate Microsoft.AspNetCore.Http.HttpResponse in a console/stand‑alone scenario.
public class HttpResponse
{
    // The response body stream where data will be written.
    public Stream Body { get; set; } = new MemoryStream();

    // Simple header collection – only the needed "Content-Disposition" header is demonstrated.
    public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    // Content‑type of the response (e.g., "application/xml").
    public string ContentType { get; set; }
}

public static class FormDataExporter
{
    /// <summary>
    /// Streams the PDF form data (as XFDF XML) directly to the supplied <see cref="HttpResponse"/>.
    /// The method works in a console or any non‑ASP.NET environment by using the lightweight stub above.
    /// </summary>
    /// <param name="response">The response object that provides a writable <c>Body</c> stream and header collection.</param>
    /// <param name="pdfFilePath">Full path to the source PDF file.</param>
    public static void ExportFormData(HttpResponse response, string pdfFilePath)
    {
        if (response == null) throw new ArgumentNullException(nameof(response));
        if (string.IsNullOrEmpty(pdfFilePath)) throw new ArgumentException("PDF file path is required.", nameof(pdfFilePath));
        if (!File.Exists(pdfFilePath)) throw new FileNotFoundException("PDF file not found.", pdfFilePath);

        // Set response metadata – useful when the stub is later consumed by an actual web framework.
        response.ContentType = "application/xml";
        var fileName = Path.GetFileNameWithoutExtension(pdfFilePath) + ".xfdf";
        response.Headers["Content-Disposition"] = $"attachment; filename=\"{fileName}\"";

        // Load the PDF document and export its form/annotation data to XFDF.
        using (var document = new Document(pdfFilePath))
        using (var xfdfStream = new MemoryStream())
        {
            document.ExportAnnotationsToXfdf(xfdfStream);
            xfdfStream.Position = 0; // Reset before copying.
            xfdfStream.CopyTo(response.Body);
            response.Body.Flush();
        }
    }
}

// Example usage in a console application.
class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: FormDataExporter <pdf-path>");
            return;
        }

        var pdfPath = args[0];
        var response = new HttpResponse();
        FormDataExporter.ExportFormData(response, pdfPath);

        // For demonstration, write the generated XFDF to a file next to the PDF.
        var outputPath = Path.ChangeExtension(pdfPath, ".xfdf");
        response.Body.Position = 0; // Ensure we read from the beginning.
        using (var file = File.Create(outputPath))
        {
            response.Body.CopyTo(file);
        }
        Console.WriteLine($"XFDF exported to: {outputPath}");
    }
}
