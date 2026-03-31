using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Initialize the facade and bind the PDF file
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Create a dictionary with the desired rotation for page 2 (90 degrees)
        Dictionary<int, int> rotations = new Dictionary<int, int>();
        rotations.Add(2, 90);
        editor.PageRotations = rotations;

        // Apply the rotation and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine("Page 2 rotated and saved to " + outputPath);
    }
}