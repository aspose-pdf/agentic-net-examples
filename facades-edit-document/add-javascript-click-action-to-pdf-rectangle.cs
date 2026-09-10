using System;
using System.IO;
using System.Drawing;               // Rectangle and Color for the facade API
using Aspose.Pdf.Facades;           // PdfContentEditor

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

        // Create the content editor and bind the existing PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the clickable rectangle (x, y, width, height) on page 1
        Rectangle rect = new Rectangle(100, 500, 200, 100);

        // JavaScript code to be executed when the rectangle is clicked
        string jsCode = "app.alert('Hello from Aspose.Pdf!');";

        // Attach the JavaScript link; the rectangle will be highlighted in red
        editor.CreateJavaScriptLink(jsCode, rect, 1, Color.Red);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"JavaScript link added and saved to '{outputPath}'.");
    }
}