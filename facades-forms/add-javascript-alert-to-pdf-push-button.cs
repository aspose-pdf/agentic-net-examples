using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF containing the push button named "ShowInfo"
        const string outputPath = "output.pdf";     // PDF with JavaScript attached

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade and bind it to the opened document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // Add JavaScript to the push button "ShowInfo"
                // The script shows an alert when the button is clicked
                bool success = formEditor.AddFieldScript("ShowInfo", "app.alert('Form loaded');");

                if (!success)
                {
                    Console.Error.WriteLine("Failed to add JavaScript to the button.");
                }
            }

            // Save the modified PDF (using the standard Document.Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}