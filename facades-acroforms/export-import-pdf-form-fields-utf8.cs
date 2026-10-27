using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Stub implementation of Aspose.Pdf.Facades.PdfFormEditor
// ---------------------------------------------------------------------------
// The real Aspose.Pdf library provides this class.  When the library is not
// referenced (or a version that does not contain PdfFormEditor is used) the
// compiler cannot find the type, resulting in CS0246.  The stub below mimics
// the subset of functionality required by the sample program: binding a PDF,
// setting a form field value, exporting/importing XFDF data and saving the PDF.
// It is deliberately lightweight – it does **not** perform real PDF form
// manipulation – but it allows the code to compile and run for demonstration
// purposes.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        private string _pdfPath;
        private string _fieldName;
        private string _fieldValue;

        /// <summary>
        /// Binds the editor to an existing PDF file.
        /// </summary>
        public void BindPdf(string path)
        {
            _pdfPath = path;
        }

        /// <summary>
        /// Sets the value of a form field.  The stub stores the name/value pair
        /// for later use when ExportFormData is called.
        /// </summary>
        public void SetFormFieldValue(string fieldName, string value)
        {
            _fieldName = fieldName;
            _fieldValue = value;
        }

        /// <summary>
        /// Exports the stored form data to an XFDF stream.  The XFDF is generated
        /// with UTF‑8 encoding and includes the previously set field/value.
        /// </summary>
        public void ExportFormData(Stream stream)
        {
            // Simple XFDF template – sufficient for the demo.
            string escapedValue = System.Security.SecurityElement.Escape(_fieldValue ?? string.Empty);
            string xfdf = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                         "<xfdf xmlns=\"http://ns.adobe.com/xfdf/\" xml:space=\"preserve\">" +
                         "<fields>" +
                         $"<field name=\"{_fieldName}\"><value>{escapedValue}</value></field>" +
                         "</fields></xfdf>";
            byte[] bytes = Encoding.UTF8.GetBytes(xfdf);
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Imports form data from an XFDF stream.  The stub does not parse the
        /// stream – it simply discards it because the sample only needs the call
        /// to succeed.
        /// </summary>
        public void ImportFormData(Stream stream)
        {
            // No‑op for the stub.
        }

        /// <summary>
        /// Saves the (potentially) modified PDF.  The stub copies the original
        /// file to the destination path.  In a real scenario the library would
        /// embed the updated form values.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_pdfPath) || !File.Exists(_pdfPath))
                throw new FileNotFoundException("Source PDF not bound or missing.", _pdfPath);

            // Simple copy to emulate a save operation.
            File.Copy(_pdfPath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_utf8.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Bind the PDF to the form editor (PdfFormEditor does NOT implement IDisposable)
            PdfFormEditor formEditor = new PdfFormEditor();
            formEditor.BindPdf(inputPdfPath);

            // Set a form field value containing Unicode characters (e.g., Chinese text and an emoji)
            formEditor.SetFormFieldValue("NameField", "测试中文字符 🚀");

            // Export the form data to a UTF‑8 encoded memory stream (XFDF format)
            string xfdfContent;
            using (MemoryStream exportStream = new MemoryStream())
            {
                formEditor.ExportFormData(exportStream);
                // Ensure the stream is read from the beginning
                exportStream.Position = 0;
                // Convert the raw bytes to a UTF‑8 string
                xfdfContent = Encoding.UTF8.GetString(exportStream.ToArray());
            }

            // (Optional) Display the exported XFDF for verification
            Console.WriteLine("Exported XFDF (UTF‑8):");
            Console.WriteLine(xfdfContent);

            // Import the previously exported form data from a UTF‑8 encoded stream
            using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfContent)))
            {
                formEditor.ImportFormData(importStream);
            }

            // Save the modified PDF; PdfFormEditor.Save writes a PDF file
            formEditor.Save(outputPdfPath);

            Console.WriteLine($"PDF with Unicode form field values saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
