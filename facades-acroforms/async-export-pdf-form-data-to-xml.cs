using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

// Stub implementation for environments where the Aspose.Pdf library is not referenced.
// In a real project, remove this stub and reference the official Aspose.Pdf NuGet package.
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub of the Aspose.Pdf.Facades.PdfFormEditor class.
    /// Provides only the members used in this sample.
    /// </summary>
    public class PdfFormEditor
    {
        private string _pdfPath;

        /// <summary>
        /// Binds the specified PDF file to the editor.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            // In the real library this loads the PDF document.
            // Here we simply store the path for demonstration purposes.
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");
            _pdfPath = pdfPath;
        }

        /// <summary>
        /// Exports form data to an XML file.
        /// The stub writes a very simple XML placeholder.
        /// </summary>
        public void ExportFormDataToXml(string xmlPath)
        {
            // Real implementation would extract form fields and write them to XML.
            // This stub creates a minimal XML document so the sample runs without the actual library.
            string xmlContent = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<FormData>\n  <SourcePdf>{System.Security.SecurityElement.Escape(_pdfPath ?? string.Empty)}</SourcePdf>\n</FormData>";
            File.WriteAllText(xmlPath, xmlContent);
        }
    }
}

class Program
{
    // Asynchronous entry point (C# 7.1+)
    static async Task Main(string[] args)
    {
        // Example input PDF containing form fields and output XML file
        const string inputPdfPath  = "form.pdf";
        const string outputXmlPath = "formData.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Perform the export without blocking the UI / console thread
            await ExportFormDataToXmlAsync(inputPdfPath, outputXmlPath);
            Console.WriteLine($"Form data exported to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }

    /// <summary>
    /// Exports form data from a PDF to an XML file asynchronously.
    /// The heavy I/O work is offloaded to a background thread via Task.Run,
    /// keeping the calling thread responsive.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="xmlPath">Path where the XML output will be saved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static Task ExportFormDataToXmlAsync(string pdfPath, string xmlPath)
    {
        // Use Task.Run to execute the blocking Facades API on a thread‑pool thread
        return Task.Run(() =>
        {
            // PdfFormEditor does not implement IDisposable, so no using block is required
            PdfFormEditor formEditor = new PdfFormEditor();

            // Bind the PDF document to the editor
            formEditor.BindPdf(pdfPath);

            // Export the form fields data to the specified XML file
            formEditor.ExportFormDataToXml(xmlPath);
        });
    }
}
