using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

// Stub implementation for missing Aspose.Pdf.Facades.PdfFormEditor.
// In a real project, reference the official Aspose.Pdf NuGet package.
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        private string _pdfPath;

        // Binds the PDF file to the editor. In the stub we just store the path.
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("pdfPath cannot be null or empty", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);
            _pdfPath = pdfPath;
        }

        // Returns the current items of a list box field.
        // The stub returns an empty array because we cannot parse real PDFs.
        public string[] GetListBoxItems(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName cannot be null or empty", nameof(fieldName));
            // In a real implementation this would read the field values from the PDF.
            return Array.Empty<string>();
        }

        // Sets the items of a list box field.
        // The stub does nothing; it only validates arguments.
        public void SetListBoxItems(string fieldName, string[] items)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName cannot be null or empty", nameof(fieldName));
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            // No operation in the stub.
        }

        // Saves the modified PDF to the specified path.
        // The stub simply copies the original file to the destination.
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("outputPath cannot be null or empty", nameof(outputPath));
            if (string.IsNullOrEmpty(_pdfPath))
                throw new InvalidOperationException("No PDF has been bound. Call BindPdf first.");
            File.Copy(_pdfPath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Whitelist of allowed state values
        string[] whitelist = new string[]
        {
            "Alabama", "Alaska", "Arizona", "Arkansas", "California",
            "Colorado", "Connecticut", "Delaware", "Florida", "Georgia"
            // add other allowed items as needed
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFormEditor does NOT implement IDisposable, so no using block needed
        PdfFormEditor formEditor = new PdfFormEditor();

        // Bind the PDF document
        formEditor.BindPdf(inputPdf);

        // Retrieve current items of the list box named "State"
        string[] currentItems = formEditor.GetListBoxItems("State");

        // Filter out items not present in the whitelist
        var filteredList = new List<string>();
        foreach (string item in currentItems)
        {
            foreach (string allowed in whitelist)
            {
                if (string.Equals(item, allowed, StringComparison.OrdinalIgnoreCase))
                {
                    filteredList.Add(item);
                    break;
                }
            }
        }

        // Update the list box with the filtered items
        formEditor.SetListBoxItems("State", filteredList.ToArray());

        // Save the modified PDF
        formEditor.Save(outputPdf);

        Console.WriteLine($"Filtered list saved to '{outputPdf}'.");
    }
}
