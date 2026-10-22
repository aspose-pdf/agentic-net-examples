using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub definitions to allow compilation when the Aspose.Pdf.Facades assembly is not referenced.
// In a real project the NuGet package Aspose.Pdf should be added, which provides the actual implementation.
namespace Aspose.Pdf.Facades
{
    // This stub mimics the public API used in this sample. It does nothing but allows the code to compile.
    public class PdfFormEditor
    {
        public void BindPdf(string path) { /* real implementation binds the PDF */ }
        public void SetJavaScript(string fieldName, string javaScript) { /* real implementation attaches JS */ }
        public void Save(string outputPath) { /* real implementation saves the PDF */ }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to the form editor (PdfFormEditor is not IDisposable)
        PdfFormEditor formEditor = new PdfFormEditor();
        formEditor.BindPdf(inputPath);

        // JavaScript that shows an alert when the button is clicked
        string js = "app.alert('Form loaded');";

        // Attach the JavaScript to the push button named "ShowInfo"
        formEditor.SetJavaScript("ShowInfo", js);

        // Save the modified PDF with the attached script
        formEditor.Save(outputPath);

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
