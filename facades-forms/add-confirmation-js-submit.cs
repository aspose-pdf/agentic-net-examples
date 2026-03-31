using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string buttonName = "btnSubmit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        // Initialize FormEditor with source and destination files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // JavaScript that asks for confirmation before submitting the form
        string jsCode = "if(app.alert('Do you want to submit the form?', 2, 0) == 4) { this.submitForm(); }";

        // Attach the script to the submit button
        bool scriptAdded = formEditor.SetFieldScript(buttonName, jsCode);
        if (!scriptAdded)
        {
            Console.Error.WriteLine("Failed to attach JavaScript to button: " + buttonName);
        }
        else
        {
            Console.WriteLine("JavaScript attached to button successfully.");
        }

        // Save the modified PDF (if the Save method is available)
        // Some versions of FormEditor write changes automatically when the object is disposed.
        // The following call ensures the output file is written.
        try
        {
            formEditor.Save(outputPath);
        }
        catch (Exception)
        {
            // If Save is not supported, the changes are already saved by the constructor.
        }
    }
}