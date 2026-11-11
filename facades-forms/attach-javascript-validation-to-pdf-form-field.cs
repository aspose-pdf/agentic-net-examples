using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // JavaScript that shows a warning if the entered age is less than 18
            string js = "if (event.value < 18) { app.alert('Age must be at least 18.'); }";

            // FormEditor works with PDF forms; it does not implement IDisposable
            FormEditor editor = new FormEditor();
            editor.BindPdf(inputPath);                     // Load the PDF
            editor.AddFieldScript("Age", js);              // Attach JS to the field named "Age"
            editor.Save(outputPath);                       // Save the modified PDF
            editor.Close();                                // Release resources

            Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
