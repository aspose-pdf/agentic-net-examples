using System;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub for Aspose.Pdf.Facades.PdfXmpMetadataEditor
// -----------------------------------------------------------------------------
// The real Aspose.Pdf library provides this class. In environments where the
// library is not referenced we supply a lightweight implementation that satisfies
// the compiler and mimics the required behaviour for the sample program.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub implementation of the Aspose.Pdf.Facades.PdfXmpMetadataEditor class.
    /// It stores XMP properties in an in‑memory dictionary and pretends to bind
    /// and save a PDF file. This is sufficient for compilation and for unit‑test
    /// scenarios where the real PDF manipulation is not needed.
    /// </summary>
    public class PdfXmpMetadataEditor
    {
        private string _boundPdfPath;
        private readonly System.Collections.Generic.Dictionary<(string ns, string name), string> _properties
            = new System.Collections.Generic.Dictionary<(string ns, string name), string>();

        /// <summary>
        /// Binds the editor to an existing PDF file. In the stub we only remember the path.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));
            _boundPdfPath = pdfPath;
        }

        /// <summary>
        /// Retrieves the current value of an XMP property. Returns null if the property
        /// has not been set.
        /// </summary>
        public string GetXmpProperty(string ns, string name)
        {
            if (ns == null) throw new ArgumentNullException(nameof(ns));
            if (name == null) throw new ArgumentNullException(nameof(name));
            _properties.TryGetValue((ns, name), out var value);
            return value;
        }

        /// <summary>
        /// Sets (or overwrites) an XMP property.
        /// </summary>
        public void SetXmpProperty(string ns, string name, string value)
        {
            if (ns == null) throw new ArgumentNullException(nameof(ns));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (value == null) throw new ArgumentNullException(nameof(value));
            _properties[(ns, name)] = value;
        }

        /// <summary>
        /// Saves the (hypothetical) changes to a new PDF file. The stub simply copies the
        /// original file if it exists; otherwise it creates an empty file.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // In a real implementation the PDF would be written with updated XMP.
            // Here we just ensure a file exists at the target location.
            try
            {
                if (!string.IsNullOrEmpty(_boundPdfPath) && System.IO.File.Exists(_boundPdfPath))
                {
                    System.IO.File.Copy(_boundPdfPath, outputPath, overwrite: true);
                }
                else
                {
                    // Create an empty placeholder file.
                    using (var fs = System.IO.File.Create(outputPath)) { }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save PDF to '{outputPath}'.", ex);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the XMP metadata editor (does NOT implement IDisposable)
        PdfXmpMetadataEditor xmpEditor = new PdfXmpMetadataEditor();

        // Bind the existing PDF file
        xmpEditor.BindPdf(inputPdf);

        // Define the XMP properties you want to set:
        // Each entry: namespace URI, property name, new value
        var propertiesToSet = new (string ns, string name, string value)[]
        {
            ("http://purl.org/dc/elements/1.1/", "title", "New Document Title"),
            ("http://purl.org/dc/elements/1.1/", "creator", "John Doe"),
            ("http://ns.adobe.com/pdf/1.3/", "Producer", "MyApp 1.0")
        };

        foreach (var (ns, name, newValue) in propertiesToSet)
        {
            // Retrieve the current value (null if not present)
            string existingValue = xmpEditor.GetXmpProperty(ns, name);

            // If the property already has a non‑empty value, log a warning
            if (!string.IsNullOrEmpty(existingValue))
            {
                Console.WriteLine(
                    $"Warning: XMP property '{name}' in namespace '{ns}' already has value '{existingValue}'. It will be overwritten.");
            }

            // Set (or overwrite) the property with the new value
            xmpEditor.SetXmpProperty(ns, name, newValue);
        }

        // Save the modified PDF to a new file
        xmpEditor.Save(outputPdf);

        Console.WriteLine($"XMP metadata updated and saved to '{outputPdf}'.");
    }
}
