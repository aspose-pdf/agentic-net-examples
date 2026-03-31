using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Attach JavaScript alert to the push button named "ShowInfo"
        // The script will display "Form loaded" when the button is clicked
        bool scriptAdded = formEditor.SetFieldScript("ShowInfo", "app.alert('Form loaded');");
        if (!scriptAdded)
        {
            Console.Error.WriteLine("Failed to attach JavaScript to the button.");
        }

        // Finalize changes and release resources
        formEditor.Close();
        Console.WriteLine($"JavaScript attached. Output saved to '{outputPath}'.");
    }
}
