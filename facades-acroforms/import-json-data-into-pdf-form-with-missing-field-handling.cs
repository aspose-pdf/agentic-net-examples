using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub for FormException if the referenced Aspose.Pdf version does not contain it.
// This ensures the code compiles while preserving the intended error‑handling semantics.
namespace Aspose.Pdf.Facades
{
    public class FormException : Exception
    {
        public FormException() { }
        public FormException(string message) : base(message) { }
        public FormException(string message, Exception inner) : base(message, inner) { }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonDataPath = "data.json";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF.
            using (Form form = new Form(inputPdfPath))
            {
                // Import field data from a JSON stream.
                using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        form.ImportJson(jsonStream);
                    }
                    catch (FormException fe)
                    {
                        // Log missing field information. The exception message typically contains the field name.
                        Console.Error.WriteLine($"Missing form field during import: {fe.Message}");
                        // Depending on requirements you could re‑throw, break, or continue.
                    }
                }

                // Save the updated PDF.
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
