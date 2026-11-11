using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the existing PDF to the form editor
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // JavaScript that shows an alert when the button is clicked
                string js = "app.alert('Form loaded');";

                // Attach the JavaScript to the push button named "ShowInfo"
                // FormEditor does not have AddJavaScript; use AddFieldScript instead.
                formEditor.AddFieldScript("ShowInfo", js);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
