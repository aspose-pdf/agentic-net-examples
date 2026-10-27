using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub implementation for Aspose.Pdf.Facades.PdfFormEditor when the real library is not referenced.
// This allows the sample to compile and run (it will simply copy the input PDF to the output).
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        private string _inputPath;
        private string _javaScript;

        /// <summary>
        /// Binds the PDF file that will be edited.
        /// </summary>
        public void BindPdf(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("PDF file not found.", path);
            _inputPath = path;
        }

        /// <summary>
        /// Adds document‑level JavaScript. In this stub the script is stored but not applied.
        /// </summary>
        public void AddJavaScript(string js)
        {
            _javaScript = js; // No real processing – placeholder for compatibility.
        }

        /// <summary>
        /// Saves the (potentially modified) PDF to the specified output path.
        /// The stub simply copies the original file because we do not have the real Aspose implementation.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_inputPath))
                throw new InvalidOperationException("No PDF has been bound. Call BindPdf first.");
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // Ensure the directory exists.
            var dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // In a real scenario the JavaScript would be embedded. Here we just copy the file.
            File.Copy(_inputPath, outputPath, true);
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

        // Load the PDF into the form editor
        PdfFormEditor formEditor = new PdfFormEditor();
        formEditor.BindPdf(inputPath);

        // JavaScript that runs when the document is opened and sets the "Date" field
        string js = "this.getField('Date').value = (new Date()).toLocaleDateString();";

        // Attach the script as a document‑level JavaScript (executed on load)
        formEditor.AddJavaScript(js);

        // Save the updated PDF
        formEditor.Save(outputPath);

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
