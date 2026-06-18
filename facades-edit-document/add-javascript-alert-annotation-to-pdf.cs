using System;
using System.IO;
using System.Drawing;               // Rectangle and Color are defined here
using Aspose.Pdf.Facades;          // PdfContentEditor resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string jsCode     = "app.alert('Hello from Aspose.Pdf!');";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the clickable area (x, y, width, height)
        // Adjust the coordinates as needed for your document
        Rectangle rect = new Rectangle(100, 500, 200, 50); // left=100, top=500, width=200, height=50

        // Create a JavaScript link that shows an alert when the annotation is activated
        editor.CreateJavaScriptLink(jsCode, rect, 1, Color.Red);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"JavaScript annotation added and saved to '{outputPath}'.");
    }
}