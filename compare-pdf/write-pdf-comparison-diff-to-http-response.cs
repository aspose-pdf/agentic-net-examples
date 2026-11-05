using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

// Minimal stub to emulate Microsoft.AspNetCore.Http.HttpResponse in a console app.
public class HttpResponse
{
    public int StatusCode { get; set; }
    public string ContentType { get; set; }
    public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
    public Stream Body { get; } = new MemoryStream();

    public async Task WriteAsync(string text)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(text);
        await Body.WriteAsync(bytes, 0, bytes.Length);
    }
}

public class PdfComparisonHandler
{
    // Writes a visual side‑by‑side diff PDF directly to the provided HttpResponse.Body stream.
    // leftPdfPath  – path to the first PDF (reference).
    // rightPdfPath – path to the second PDF (to compare against the reference).
    public async Task CompareAndWriteAsync(HttpResponse response, string leftPdfPath, string rightPdfPath)
    {
        // Validate input files.
        if (!File.Exists(leftPdfPath))
        {
            response.StatusCode = 404;
            await response.WriteAsync($"File not found: {leftPdfPath}");
            return;
        }

        if (!File.Exists(rightPdfPath))
        {
            response.StatusCode = 404;
            await response.WriteAsync($"File not found: {rightPdfPath}");
            return;
        }

        // Set HTTP‑like headers for a PDF download.
        response.ContentType = "application/pdf";
        response.Headers["Content-Disposition"] = "attachment; filename=\"diff.pdf\"";

        // Load both documents inside using blocks to ensure deterministic disposal.
        using (Document referenceDoc = new Document(leftPdfPath))
        using (Document compareDoc = new Document(rightPdfPath))
        {
            // Use the static SideBySidePdfComparer to write the diff PDF directly to the response stream.
            // No intermediate file is created.
            SideBySidePdfComparer.Compare(
                referenceDoc,
                compareDoc,
                response.Body,
                new SideBySideComparisonOptions { AdditionalChangeMarks = true }
            );
        }

        // Ensure all data is flushed to the client (or, in this stub, to the MemoryStream).
        await response.Body.FlushAsync();
    }
}

// Example usage in a console application.
class Program
{
    static async Task Main()
    {
        var handler = new PdfComparisonHandler();
        var response = new HttpResponse();
        await handler.CompareAndWriteAsync(response, "left.pdf", "right.pdf");

        // For demonstration, write the generated diff PDF to a file.
        File.WriteAllBytes("diff.pdf", ((MemoryStream)response.Body).ToArray());
        Console.WriteLine("Diff PDF written to diff.pdf");
    }
}
