using System;
using System.Drawing;
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

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the clickable rectangle (x, y, width, height) in points
        Rectangle rect = new Rectangle(100, 500, 200, 100);
        string javaScriptCode = "app.alert('Hello from Aspose.Pdf!');";
        editor.CreateJavaScriptLink(javaScriptCode, rect, 1, Color.Red);

        editor.Save(outputPath);
        Console.WriteLine($"JavaScript link added and saved to '{outputPath}'.");
    }
}