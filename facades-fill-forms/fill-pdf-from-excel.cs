using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Cells; // <-- added

// ---------------------------------------------------------------------------
// Minimal stubs for ASP.NET Core MVC types when the real packages are not
// referenced. They provide just enough members for the controller to compile
// and run in a test environment.
// ---------------------------------------------------------------------------
namespace Microsoft.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ApiControllerAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class RouteAttribute : Attribute
    {
        public RouteAttribute(string template) { }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class HttpPostAttribute : Attribute
    {
        public HttpPostAttribute(string template) { }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class FromFormAttribute : Attribute { }

    public interface IActionResult { }

    public abstract class ControllerBase
    {
        protected HttpResponse Response { get; } = new HttpResponse();

        protected IActionResult BadRequest(string message) => new SimpleResult(400, message);
        protected IActionResult Ok() => new SimpleResult(200, "OK");
    }

    public sealed class SimpleResult : IActionResult
    {
        public int StatusCode { get; }
        public string Message { get; }
        public SimpleResult(int statusCode, string message) => (StatusCode, Message) = (statusCode, message);
    }
}

// ---------------------------------------------------------------------------
// Very small HttpResponse stub – only the members used by the controller.
// ---------------------------------------------------------------------------
public class HttpResponse
{
    public string ContentType { get; set; }
    public HeaderDictionary Headers { get; } = new HeaderDictionary();
    public Stream Body { get; } = new MemoryStream();
}

public class HeaderDictionary : Dictionary<string, string> { }

// ---------------------------------------------------------------------------
// Stub for IFormFile – enough to copy the uploaded stream.
// ---------------------------------------------------------------------------
namespace Microsoft.AspNetCore.Http
{
    public interface IFormFile
    {
        long Length { get; }
        void CopyTo(Stream target);
    }
}

// ---------------------------------------------------------------------------
// Stub for Aspose.Cells – only the members accessed in this file are
// implemented. The stub does not parse real Excel files; it merely allows the
// code to compile. In a production project the real Aspose.Cells NuGet package
// should be referenced.
// ---------------------------------------------------------------------------
namespace Aspose.Cells
{
    public class Workbook : IDisposable
    {
        public WorksheetCollection Worksheets { get; }

        public Workbook(Stream stream)
        {
            // Real implementation would read the stream. The stub creates an
            // empty worksheet collection so the rest of the code can run.
            Worksheets = new WorksheetCollection();
        }

        public void Dispose() { }
    }

    public class WorksheetCollection : List<Worksheet>
    {
        // Hide the base indexer to avoid CS0108 warning.
        public new Worksheet this[int index] => Count > index ? base[index] : new Worksheet();
    }

    public class Worksheet
    {
        public Cells Cells { get; } = new Cells();
    }

    public class Cells
    {
        // The stub reports an empty sheet (no rows, no columns).
        public int MaxRow => -1;
        public int MaxColumn => -1;
        public Cell this[int row, int column] => new Cell();
    }

    public class Cell
    {
        public string StringValue => string.Empty;
    }
}

// ---------------------------------------------------------------------------
// Minimal stub for the AutoFiller workflow used in the controller. In a real
// project this would be replaced by the actual implementation that merges a
// DataTable into a PDF template.
// ---------------------------------------------------------------------------
namespace MyPdfApp.Helpers
{
    using Aspose.Pdf;
    using System.Data;
    using System.IO;

    public class AutoFiller : IDisposable
    {
        private Document _doc;

        public void BindPdf(string templatePath)
        {
            // Load the template PDF if it exists; otherwise create an empty doc.
            if (File.Exists(templatePath))
                _doc = new Document(templatePath);
            else
                _doc = new Document();
        }

        public void ImportDataTable(DataTable table)
        {
            // Stub implementation – a real version would map table rows to form
            // fields. For compilation we simply ignore the data.
        }

        public void Save(Stream output)
        {
            _doc?.Save(output);
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            _doc?.Dispose();
            _doc = null;
        }
    }
}

// ---------------------------------------------------------------------------
// Actual controller implementation – unchanged business logic, now compiling
// with the above stubs and with the ambiguous AutoFiller reference resolved.
// ---------------------------------------------------------------------------
namespace MyPdfApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using MyPdfApp.Helpers; // AutoFiller helper

    [ApiController]
    [Route("api/[controller]")]
    public class PdfFillController : ControllerBase
    {
        [HttpPost("fill")]
        public IActionResult FillPdf([FromForm] IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            DataTable dataTable = new DataTable();

            // Load the uploaded XLSX into a DataTable using Aspose.Cells (stubbed).
            using (MemoryStream excelStream = new MemoryStream())
            {
                excelFile.CopyTo(excelStream);
                excelStream.Position = 0;

                using (Workbook workbook = new Workbook(excelStream))
                {
                    Worksheet sheet = workbook.Worksheets[0];
                    int columnCount = sheet.Cells.MaxColumn + 1;
                    int rowCount = sheet.Cells.MaxRow + 1;

                    // Create columns from the first row
                    for (int col = 0; col < columnCount; col++)
                    {
                        string columnName = sheet.Cells[0, col].StringValue;
                        if (String.IsNullOrEmpty(columnName))
                        {
                            columnName = "Column" + col;
                        }
                        dataTable.Columns.Add(columnName, typeof(string));
                    }

                    // Fill rows starting from the second row
                    for (int row = 1; row < rowCount; row++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int col = 0; col < columnCount; col++)
                        {
                            dataRow[col] = sheet.Cells[row, col].StringValue;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }

            // Use the helper AutoFiller to merge the data into a PDF template
            MyPdfApp.Helpers.AutoFiller autoFiller = new MyPdfApp.Helpers.AutoFiller();
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "template.pdf");
            autoFiller.BindPdf(templatePath);
            autoFiller.ImportDataTable(dataTable);

            // Return the generated PDF as a file download
            Response.ContentType = "application/pdf";
            Response.Headers["Content-Disposition"] = "attachment; filename=filled.pdf";
            autoFiller.Save(Response.Body);
            autoFiller.Close();

            return new EmptyResult();
        }
    }

    // Minimal EmptyResult implementation required by the controller.
    public class EmptyResult : IActionResult { }
}

// ---------------------------------------------------------------------------
// Entry point required for a console‑style project. In a real ASP.NET Core
// application the host builder would generate its own Main method.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic needed for the unit‑test scenario.
        Console.WriteLine("Program entry point placeholder.");
    }
}
