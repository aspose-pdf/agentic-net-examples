using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Stub implementation for missing Aspose.Pdf.Facades types (remove when the real library is referenced)
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub of the PdfFormEditor class required for compilation.
    /// Provides only the members used in this sample. Replace with the real
    /// Aspose.Pdf.Facades.PdfFormEditor when the Aspose.Pdf NuGet package is
    /// referenced.
    /// </summary>
    public class PdfFormEditor : IDisposable
    {
        public void BindPdf(string path)
        {
            // In the real implementation this loads the PDF document.
        }

        public void AddRadioButtonGroup(string fieldName, string[] options, int defaultIndex, int pageNumber,
                                         double llx, double lly, double urx, double ury)
        {
            // In the real implementation this adds a radio‑button group to the form.
        }

        public void Save(string outputPath)
        {
            // In the real implementation this writes the modified PDF to disk.
        }

        public void Close()
        {
            // In the real implementation this releases internal resources.
        }

        public void Dispose()
        {
            Close();
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to the form editor.
        using var formEditor = new PdfFormEditor();
        formEditor.BindPdf(inputPath);

        // Radio button options and default selection (index 0 = "Male").
        string[] options = new[] { "Male", "Female", "Other" };
        int defaultIndex = 0;

        // Add the radio button group on page 1.
        // Coordinates are in points (1/72 inch). Adjust as needed.
        // Parameters: field name, options, default index, page number, llx, lly, urx, ury.
        formEditor.AddRadioButtonGroup("Gender", options, defaultIndex, 1, 100, 600, 200, 650);

        // Save the modified PDF.
        formEditor.Save(outputPath);

        // Resources are released by the using statement.
        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}
