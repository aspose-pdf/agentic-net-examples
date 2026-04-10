using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor resides here

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

        // Bind the existing PDF to a FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Attach JavaScript to the push button named "ShowInfo"
            // The script shows an alert when the button is clicked
            bool added = formEditor.AddFieldScript("ShowInfo", "app.alert('Form loaded');");
            if (!added)
            {
                Console.Error.WriteLine("Failed to add JavaScript to the button.");
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
    }
}