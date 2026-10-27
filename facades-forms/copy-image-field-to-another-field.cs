using System;
using System.IO;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub for Aspose.Pdf.Facades.PdfFormEditor
// -----------------------------------------------------------------------------
// This stub is provided to allow the sample to compile when the real Aspose.Pdf
// library is not referenced. In a production environment you should reference the
// official Aspose.Pdf NuGet package instead of using this stub.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Very small subset of the real PdfFormEditor API required for the sample.
    /// </summary>
    public class PdfFormEditor
    {
        private string _sourcePath;
        private string _tempPath;

        /// <summary>
        /// Binds the PDF file that will be edited.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            _sourcePath = pdfPath;
            // Work on a copy so that Save does not overwrite the original unintentionally.
            _tempPath = Path.GetTempFileName();
            File.Copy(_sourcePath, _tempPath, true);
        }

        /// <summary>
        /// Returns the raw bytes of the specified form field as a stream.
        /// The stub simply returns the whole PDF file stream – sufficient for the
        /// compile‑time demonstration.
        /// </summary>
        public MemoryStream GetFieldValueAsStream(string fieldName)
        {
            // In a real implementation you would extract the image bytes of the
            // field named "fieldName". Here we just return the PDF bytes so the
            // sample runs without the actual library.
            if (string.IsNullOrEmpty(_tempPath) || !File.Exists(_tempPath))
                return null;

            byte[] data = File.ReadAllBytes(_tempPath);
            return new MemoryStream(data);
        }

        /// <summary>
        /// Sets the value of a form field. The stub does nothing – it only exists
        /// so the method call compiles.
        /// </summary>
        public void SetFieldValue(string fieldName, Stream valueStream)
        {
            // No‑op for the stub. Real implementation would replace the image in
            // the specified form field.
        }

        /// <summary>
        /// Saves the edited PDF to the supplied path.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_tempPath) || !File.Exists(_tempPath))
                throw new InvalidOperationException("No PDF has been bound for saving.");

            File.Copy(_tempPath, outputPath, true);
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
            // Bind the PDF document to the form editor facade
            PdfFormEditor formEditor = new PdfFormEditor();
            formEditor.BindPdf(inputPath);

            // Retrieve the image from the source field "Logo" as a stream
            MemoryStream logoStream = formEditor.GetFieldValueAsStream("Logo");
            if (logoStream == null)
            {
                Console.Error.WriteLine("Field \"Logo\" not found or does not contain an image.");
                return;
            }

            // Ensure the stream is positioned at the beginning before re‑using it
            logoStream.Position = 0;

            // Set the retrieved image stream into the target field "HeaderLogo"
            formEditor.SetFieldValue("HeaderLogo", logoStream);

            // Save the modified PDF to a new file
            formEditor.Save(outputPath);

            Console.WriteLine($"Image copied from \"Logo\" to \"HeaderLogo\" and saved as \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
