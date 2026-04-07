using System;
using System.IO;
using System.Web;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Stubs for System.Web types when the project targets .NET Core / .NET 5+.
// These are only compiled if the real System.Web assembly is not available.
// -----------------------------------------------------------------------------
#if !NETFRAMEWORK
namespace System.Web
{
    public class HttpResponse
    {
        // In a real ASP.NET environment this would be the response stream.
        // For console builds we simply use the standard output stream.
        public Stream OutputStream { get; } = Console.OpenStandardOutput();
        public void Clear() { /* no‑op */ }
        public string ContentType { get; set; }
        public void AddHeader(string name, string value) { /* no‑op */ }
        public void Flush() { /* no‑op */ }
        public void End() { /* no‑op */ }
    }

    public class HttpServerUtility
    {
        // Very simple virtual‑path mapper – just strips the leading "~/" and
        // replaces forward slashes with the OS‑specific directory separator.
        public string MapPath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return string.Empty;
            var trimmed = virtualPath.TrimStart('~', '/');
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, trimmed.Replace('/', Path.DirectorySeparatorChar));
        }
    }

    public class HttpContext
    {
        // Singleton used by the sample code (HttpContext.Current).
        public static HttpContext Current { get; } = new HttpContext();
        public HttpResponse Response { get; } = new HttpResponse();
        public HttpServerUtility Server { get; } = new HttpServerUtility();
    }
}
#endif

// -----------------------------------------------------------------------------
// The actual handler that streams form data as XFDF (XML) to the client.
// -----------------------------------------------------------------------------
public class FormExportHandler
{
    /// <summary>
    /// Streams the XML representation of the PDF form fields to the HTTP response.
    /// Call this method from an ASP.NET page, MVC controller, or any HTTP handler.
    /// </summary>
    public void ExportFormData()
    {
        // Resolve the physical path of the PDF that contains the form.
        string pdfPath = HttpContext.Current.Server.MapPath("~/App_Data/form.pdf");

        // Bind the PDF to the Form facade. The Form object implements IDisposable,
        // so wrap it in a using block to ensure resources are released.
        using (Form form = new Form(pdfPath))
        {
            // Obtain the current HTTP response.
            HttpResponse response = HttpContext.Current.Response;

            // Prepare the response for an XML file download.
            response.Clear();
            response.ContentType = "application/xml";
            response.AddHeader("Content-Disposition", "attachment; filename=FormData.xml");

            // Export the form fields directly to the response output stream.
            // ExportXml writes the form data in XFDF (XML) format.
            form.ExportXml(response.OutputStream);

            // Flush the stream and end the response to send the file immediately.
            response.Flush();
            response.End();
        }
    }
}

// -----------------------------------------------------------------------------
// Minimal console entry point – required for projects that expect a Main method.
// The Main method does not perform any work; it simply demonstrates that the
// assembly compiles and can be executed in a non‑web context.
// -----------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // In a real web application this method would never be called.
        // It exists solely to satisfy the compiler when the project type is a
        // console application.
        Console.WriteLine("FormExportHandler compiled successfully. Use it within an ASP.NET context.");
    }
}
