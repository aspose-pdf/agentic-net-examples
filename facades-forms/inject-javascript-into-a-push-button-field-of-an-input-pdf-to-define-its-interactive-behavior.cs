using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "myButton"; // fully qualified push button field name
        const string javaScript = "app.alert('Button clicked!');";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use FormEditor facade to modify the form
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPath);

            // Attach JavaScript to the specified push button field
            bool success = formEditor.SetFieldScript(fieldName, javaScript);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set JavaScript on field '{fieldName}'.");
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript injected and saved to '{outputPath}'.");
    }
}