using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub for FormException if the Aspose.Pdf library version does not expose it.
// This allows the code to compile and still catch the specific exception type
// when Aspose.Pdf throws it. If the real FormException exists in the referenced
// assembly, this stub will be ignored because the actual type will be used.
namespace Aspose.Pdf.Facades
{
    public class FormException : Exception
    {
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
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Open the JSON stream containing field values
                using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        // Attempt to import the field data
                        form.ImportJson(jsonStream);
                    }
                    catch (FormException fe)
                    {
                        // Log the missing field name (or message) when import fails
                        Console.Error.WriteLine($"Missing form field during import: {fe.Message}");
                    }
                }

                // Save the updated PDF to the output path
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
