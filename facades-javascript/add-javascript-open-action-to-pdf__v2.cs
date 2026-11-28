using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string resultPath = "result.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(sourcePath);

        // Add a JavaScript action that will be executed when the PDF is opened
        // Example: display an alert dialog
        string js = "app.alert('Document opened via Aspose.Pdf');";
        doc.OpenAction = new JavascriptAction(js);

        // Save the modified PDF to the output path
        doc.Save(resultPath);

        Console.WriteLine($"JavaScript added and saved to '{resultPath}'.");
    }
}
