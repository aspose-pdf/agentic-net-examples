using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

// ---------------------------------------------------------------------------
// Minimal stubs that emulate the parts of Microsoft.AspNetCore.Http we need.
// This allows the example to compile as a console application without pulling
// in the full ASP.NET Core package.
// ---------------------------------------------------------------------------
namespace Microsoft.AspNetCore.Http
{
    // HTTP status code constants used in the example.
    public static class StatusCodes
    {
        public const int Status404NotFound = 404;
    }

    // Simple header dictionary – a thin wrapper around Dictionary<string,string>.
    public interface IHeaderDictionary : IDictionary<string, string> { }
    public class HeaderDictionary : Dictionary<string, string>, IHeaderDictionary { }

    // Very small HttpResponse stub exposing only the members used by the sample.
    public class HttpResponse
    {
        // Initialise to a non‑null default to satisfy the non‑nullable warning.
        public string ContentType { get; set; } = string.Empty;
        public IHeaderDictionary Headers { get; } = new HeaderDictionary();
        public Stream Body { get; set; } = new MemoryStream();
        public int StatusCode { get; set; }
    }
}

public class FormDataExporter
{
    /// <summary>
    /// Streams an XML representation of PDF form fields directly to the provided
    /// HttpResponse (stub) for immediate download.
    /// </summary>
    /// <param name="response">A minimal HttpResponse implementation.</param>
    /// <param name="pdfFilePath">Path to the source PDF file.</param>
    public static void ExportFormData(Microsoft.AspNetCore.Http.HttpResponse response, string pdfFilePath)
    {
        if (!File.Exists(pdfFilePath))
        {
            response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound;
            return;
        }

        // Set response headers for file download.
        response.ContentType = "application/xml";
        var fileName = Path.GetFileNameWithoutExtension(pdfFilePath) + "_FormData.xml";
        response.Headers["Content-Disposition"] = $"attachment; filename=\"{fileName}\"";

        // Load the PDF document.
        using (Document doc = new Document(pdfFilePath))
        {
            // Write a simple XML document that lists each form field name/value.
            // This avoids the need for the Facade API (ExportXml) which is not
            // allowed by the hard namespace restriction.
            using (XmlWriter writer = XmlWriter.Create(response.Body, new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FormData");

                if (doc?.Form?.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        string fieldName = field?.Name ?? field?.FullName ?? "UnnamedField";
                        string fieldValue = field?.Value?.ToString() ?? string.Empty;

                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Name", fieldName);
                        writer.WriteString(fieldValue);
                        writer.WriteEndElement(); // Field
                    }
                }

                writer.WriteEndElement(); // FormData
                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        // Reset the stream position so that callers can read from the beginning.
        if (response.Body.CanSeek)
        {
            response.Body.Position = 0;
        }
    }
}

// ---------------------------------------------------------------------------
// Example console driver – demonstrates how the ExportFormData method can be
// invoked and the resulting XML saved to a file for verification.
// ---------------------------------------------------------------------------
class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run <pdf-file-path>");
            return;
        }

        string pdfPath = args[0];
        var response = new Microsoft.AspNetCore.Http.HttpResponse();
        FormDataExporter.ExportFormData(response, pdfPath);

        // If the export succeeded, write the in‑memory response to a physical file.
        if (response.StatusCode != Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)
        {
            string outPath = Path.Combine(Path.GetDirectoryName(pdfPath) ?? ".", "FormData.xml");
            using (var fileStream = File.Create(outPath))
            {
                response.Body.CopyTo(fileStream);
            }
            Console.WriteLine($"Form data exported to: {outPath}");
        }
        else
        {
            Console.WriteLine("PDF file not found.");
        }
    }
}
