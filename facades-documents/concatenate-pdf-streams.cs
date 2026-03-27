using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for ASP.NET Core types so the project can compile without the
// full Microsoft.AspNetCore.* packages. In a real web application you would
// reference the appropriate NuGet packages (Microsoft.AspNetCore.App) and
// remove these stubs.
// ---------------------------------------------------------------------------
namespace Microsoft.AspNetCore.Mvc
{
    // Attribute used to mark a controller class.
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ApiControllerAttribute : Attribute { }

    // Attribute that defines a route template for a controller or action.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class RouteAttribute : Attribute
    {
        public string Template { get; }
        public RouteAttribute(string template) => Template = template;
    }

    // Base class for MVC controllers.
    public abstract class ControllerBase
    {
        // Helper method that mimics ControllerBase.File(...) used to return a file.
        protected IActionResult File(Stream fileStream, string contentType, string fileDownloadName)
        {
            return new FileResult(fileStream, contentType, fileDownloadName);
        }
    }

    // Simple IActionResult interface.
    public interface IActionResult { }

    // Result that represents a file being sent to the client.
    public sealed class FileResult : IActionResult, IDisposable
    {
        public Stream FileStream { get; }
        public string ContentType { get; }
        public string FileDownloadName { get; }

        public FileResult(Stream fileStream, string contentType, string fileDownloadName)
        {
            FileStream = fileStream ?? throw new ArgumentNullException(nameof(fileStream));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            FileDownloadName = fileDownloadName ?? throw new ArgumentNullException(nameof(fileDownloadName));
        }

        public void Dispose()
        {
            FileStream?.Dispose();
        }
    }
}

namespace Microsoft.AspNetCore.Http
{
    // Minimal representation of a file sent with a multipart/form‑data request.
    public interface IFormFile
    {
        // Returns a read‑only stream for the uploaded file.
        Stream OpenReadStream();
    }
}

// ---------------------------------------------------------------------------
// Actual service implementation.
// ---------------------------------------------------------------------------
namespace PdfConcatenationService
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    [ApiController]
    [Route("api/[controller]")]
    public class PdfConcatController : ControllerBase
    {
        [HttpPost("concatenate")]
        public IActionResult Concatenate([FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return new BadRequestResult("No PDF files were provided.");
            }

            // Collect input streams from the uploaded files.
            var inputStreams = new List<Stream>();
            foreach (IFormFile file in files)
            {
                Stream fileStream = file.OpenReadStream();
                inputStreams.Add(fileStream);
            }

            // Concatenate PDFs using Aspose.Pdf.Facades.PdfFileEditor.
            var outputStream = new MemoryStream();
            var fileEditor = new PdfFileEditor();
            fileEditor.Concatenate(inputStreams.ToArray(), outputStream);
            outputStream.Position = 0;

            // Dispose the input streams – the output stream is returned to the caller.
            foreach (var s in inputStreams)
            {
                s.Dispose();
            }

            return File(outputStream, "application/pdf", "concatenated.pdf");
        }
    }

    // -----------------------------------------------------------------------
    // Additional minimal MVC result types used by the controller.
    // -----------------------------------------------------------------------
    public sealed class BadRequestResult : IActionResult
    {
        public string Message { get; }
        public BadRequestResult(string message) => Message = message;
    }

    // Attribute that tells MVC to bind a parameter from form data.
    // In a full ASP.NET Core project this attribute lives in Microsoft.AspNetCore.Mvc.
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class FromFormAttribute : Attribute { }

    // Attribute that maps an action to an HTTP POST verb.
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class HttpPostAttribute : Attribute
    {
        public string Template { get; }
        public HttpPostAttribute(string template) => Template = template;
    }

    // -----------------------------------------------------------------------
    // Dummy entry point to satisfy the compiler when the project is built as an
    // executable. In a real ASP.NET Core application the entry point would be
    // provided by the WebHost/GenericHost infrastructure.
    // -----------------------------------------------------------------------
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required for the compilation‑only scenario.
        }
    }
}
