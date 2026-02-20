using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Full name of the push button field to which the script will be added
        const string buttonFieldName = "myButton";

        // JavaScript code to be executed when the button is clicked
        const string javaScript = "app.alert('Button clicked!');";

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Initialize FormEditor and bind it to the existing PDF
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // Add JavaScript to the specified push button field.
                // If the field already contains a script, the new script is appended.
                formEditor.AddFieldScript(buttonFieldName, javaScript);

                // Save the modified PDF using the standard Save method
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"JavaScript added to button '{buttonFieldName}' and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}