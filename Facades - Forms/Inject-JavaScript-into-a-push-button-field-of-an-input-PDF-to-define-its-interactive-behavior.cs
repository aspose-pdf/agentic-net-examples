using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing a push button field
        const string inputPdf = "input.pdf";
        // Output PDF with JavaScript attached to the button
        const string outputPdf = "output.pdf";
        // Fully qualified name of the push button field (adjust as needed)
        const string buttonFieldName = "myButton";
        // JavaScript code to be executed when the button is pressed
        const string jsCode = "app.alert('Button clicked!');";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF, set the JavaScript for the push button, and save the result
        using (FormEditor editor = new FormEditor())
        {
            // Initialize the facade with the source PDF
            editor.BindPdf(inputPdf);

            // Set (replace) the JavaScript action for the specified push button field
            bool success = editor.SetFieldScript(buttonFieldName, jsCode);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set script for field '{buttonFieldName}'.");
            }

            // Persist the changes to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript injected and saved to '{outputPdf}'.");
    }
}