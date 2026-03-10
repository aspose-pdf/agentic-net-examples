using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string buttonFieldName = "myButton"; // fully qualified field name
        const string jsCode = "app.alert('Button clicked!');";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor facade to modify the PDF form
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(inputPdf);

            // Attach JavaScript to the specified push button field
            bool success = editor.SetFieldScript(buttonFieldName, jsCode);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set JavaScript for field '{buttonFieldName}'.");
            }

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript injected and saved to '{outputPdf}'.");
    }
}