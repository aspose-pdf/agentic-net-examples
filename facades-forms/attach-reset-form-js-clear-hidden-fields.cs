using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and bind the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // JavaScript that resets the form and clears hidden fields
            string js = @"
                // Reset all fields to their default values
                this.resetForm();

                // Clear values of hidden fields
                var fieldNames = this.getFieldNames();
                for (var i = 0; i < fieldNames.length; i++) {
                    var f = this.getField(fieldNames[i]);
                    if (f.display == display.hidden) {
                        f.value = '';
                    }
                }
            ";

            // Attach the script to the button named "ResetForm"
            formEditor.AddFieldScript("ResetForm", js);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}