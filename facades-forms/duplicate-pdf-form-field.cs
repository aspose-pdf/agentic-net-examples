using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

// Minimal stub for PdfFormEditor when the real Aspose.Pdf.Facades assembly is not referenced.
// This stub provides the subset of functionality required by the sample program:
//   BindPdf(string), CopyField(string, string) and Save(string).
// It uses Aspose.Pdf.Document internally to manipulate form fields.
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        // The underlying Aspose.Pdf.Document. It can be null until BindPdf is called.
        private Document? _document;

        /// <summary>
        /// Loads the PDF file into an Aspose.Pdf.Document instance.
        /// </summary>
        public void BindPdf(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("PDF file not found.", path);

            _document = new Document(path);
        }

        /// <summary>
        /// Duplicates a form field definition.
        /// The new field receives the name supplied in <paramref name="newFieldName"/>.
        /// </summary>
        public void CopyField(string sourceFieldName, string newFieldName)
        {
            if (_document == null)
                throw new InvalidOperationException("PDF not bound. Call BindPdf first.");
            if (string.IsNullOrEmpty(sourceFieldName))
                throw new ArgumentException("Source field name is required.", nameof(sourceFieldName));
            if (string.IsNullOrEmpty(newFieldName))
                throw new ArgumentException("New field name is required.", nameof(newFieldName));

            // Retrieve the original field. The Form collection returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field before using field‑specific members.
            Field? srcField = _document.Form[sourceFieldName] as Field;
            if (srcField == null)
                throw new ArgumentException($"Field '{sourceFieldName}' does not exist or is not a form field.");

            // Clone the field. Aspose.Pdf.Field implements ICloneable, so we can use Clone().
            // The returned object is a WidgetAnnotation, so we cast it back to Field.
            Field? clonedField = srcField.Clone() as Field;
            if (clonedField == null)
                throw new InvalidOperationException("Failed to clone the source field.");

            // The FullName property is read‑only; to give the new field a distinct name we set the Name property.
            clonedField.Name = newFieldName;

            // Add the cloned field to the document's form collection.
            _document.Form.Add(clonedField);
        }

        /// <summary>
        /// Saves the modified PDF to the specified path.
        /// </summary>
        public void Save(string outputPath)
        {
            if (_document == null)
                throw new InvalidOperationException("No PDF loaded to save.");
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            _document.Save(outputPath);
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
            // Bind the PDF to the form editor (facade API)
            var formEditor = new PdfFormEditor();
            formEditor.BindPdf(inputPath);

            // Duplicate the field definition: copy "Signature" to "SignatureCopy"
            formEditor.CopyField("Signature", "SignatureCopy");

            // Save the modified PDF
            formEditor.Save(outputPath);

            Console.WriteLine($"Field \"Signature\" duplicated as \"SignatureCopy\" and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
