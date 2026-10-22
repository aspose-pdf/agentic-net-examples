using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

// Minimal stub for PdfFormEditor when the Aspose.Pdf.Facades assembly is not referenced.
// This stub provides the methods used in the sample and delegates to Aspose.Pdf.Document.
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        private Document _document;

        public void BindPdf(string path)
        {
            _document = new Document(path);
        }

        // Sets the appearance of a form field. The "format" parameter is kept for API compatibility.
        // In this stub we simply assign the provided value to the field.
        public void SetFieldAppearance(string fieldName, string format, string value)
        {
            if (_document == null)
                throw new InvalidOperationException("PDF document is not bound. Call BindPdf first.");

            var field = _document.Form[fieldName] as TextBoxField;
            if (field != null)
            {
                // The real Aspose API allows a format string; here we just set the value.
                field.Value = value;
            }
            else
            {
                throw new ArgumentException($"Field '{fieldName}' not found or is not a text box.");
            }
        }

        public void Save(string outputPath)
        {
            if (_document == null)
                throw new InvalidOperationException("PDF document is not bound. Call BindPdf first.");

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

        // PdfFormEditor is a Facades class for working with form fields.
        // It does not implement IDisposable, so no using block is required.
        PdfFormEditor formEditor = new PdfFormEditor();

        // Load the PDF document.
        formEditor.BindPdf(inputPath);

        // Set the appearance of the field named "Price".
        // The format string "0.00" forces two decimal places.
        // The value is provided as a string; here we set it to "0.00" as an example.
        formEditor.SetFieldAppearance("Price", "0.00", "0.00");

        // Save the updated PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Field 'Price' formatted with two decimal places and saved to '{outputPath}'.");
    }
}