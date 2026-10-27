using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfDemo
{
    // ---------------------------------------------------------------------------
    // NOTE: The following stub is provided only to make the sample compile when the
    // real Aspose.Pdf library is not referenced. In a production project you should
    // reference the official Aspose.Pdf NuGet package and remove this stub.
    // ---------------------------------------------------------------------------
    public class PdfFormEditor
    {
        private string _pdfPath;

        public void BindPdf(string pdfPath)
        {
            _pdfPath = pdfPath;
        }

        public void SetFieldAttribute(string fieldName, string attribute, string value)
        {
            // Real implementation would modify the PDF field attribute.
            // This stub simply writes a diagnostic message.
            Console.WriteLine($"[Stub] Set attribute '{{attribute}}' of field '{{fieldName}}' to '{{value}}'.");
        }

        public void Save(string outputPath)
        {
            // In the stub we just copy the source PDF to the destination path.
            if (!string.IsNullOrEmpty(_pdfPath) && File.Exists(_pdfPath))
            {
                File.Copy(_pdfPath, outputPath, overwrite: true);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output.pdf";
            const string fieldName = "Signature";
            const string borderAttr = "BorderWidth"; // attribute controlling border thickness
            const string borderVal = "2";            // thickness in points

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // PdfFormEditor does NOT implement IDisposable; instantiate directly.
            PdfFormEditor formEditor = new PdfFormEditor();

            // Load the PDF to be edited.
            formEditor.BindPdf(inputPath);

            // Apply custom border thickness to the specified form field.
            formEditor.SetFieldAttribute(fieldName, borderAttr, borderVal);

            // Save the modified PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"Border of field \"{fieldName}\" set to {borderVal} points. Saved as \"{outputPath}\".");
        }
    }
}
