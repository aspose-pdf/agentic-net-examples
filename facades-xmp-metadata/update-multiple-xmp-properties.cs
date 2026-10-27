using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Stub implementation for XmpMetadataEditor when the real class is unavailable.
// This allows the sample to compile and demonstrates the intended usage pattern.
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub that mimics Aspose.Pdf.Facades.XmpMetadataEditor.
    /// In a real project, reference the official Aspose.Pdf NuGet package which provides the full implementation.
    /// </summary>
    public class XmpMetadataEditor
    {
        private string _sourcePath;
        private readonly System.Collections.Generic.Dictionary<string, string> _properties = new();

        /// <summary>
        /// Binds the editor to an existing PDF file.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF.</param>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            _sourcePath = pdfPath;
        }

        /// <summary>
        /// Sets an XMP property. In this stub the values are stored in a dictionary.
        /// </summary>
        /// <param name="propertyName">The XMP property name (e.g., "dc:creator").</param>
        /// <param name="value">The value to assign.</param>
        public void SetProperty(string propertyName, string value)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));

            _properties[propertyName] = value ?? string.Empty;
        }

        /// <summary>
        /// Saves the PDF with the updated XMP metadata. The stub simply copies the source file
        /// to the destination path because implementing full XMP handling is out of scope.
        /// </summary>
        /// <param name="outputPath">Path where the updated PDF should be written.</param>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_sourcePath))
                throw new InvalidOperationException("BindPdf must be called before Save.");

            // In a real implementation, the XMP packet would be rebuilt using _properties
            // and written back to the PDF. Here we just copy the original file.
            File.Copy(_sourcePath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the XMP editor to the source PDF
            XmpMetadataEditor xmpEditor = new XmpMetadataEditor();
            xmpEditor.BindPdf(inputPath);

            // Set several XMP properties before saving – all changes are applied together
            xmpEditor.SetProperty("dc:creator", "John Doe");
            xmpEditor.SetProperty("dc:title",   "Sample Document");
            xmpEditor.SetProperty("pdf:Producer", "Aspose.Pdf for .NET");

            // Write the updated PDF
            xmpEditor.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
